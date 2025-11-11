using UnityEngine;

public class PivotSpringArmCamera : MonoBehaviour
{
    [Header("References")]
    public Transform target;               // Tu personaje (ej. KnightPlayer)
    public Transform cameraTransform;      // Main Camera

    [Header("Orbit controls")]
    public float mouseSensitivity = 160f;
    [Range(0.01f, 1f)] public float sensitivityMultiplier = 0.5f;
    public float pitchMin = -30f;
    public float pitchMax = 65f;
    public bool invertY = false;

    [Header("Smoothing")]
    public float inputSmooth = 10f;
    public float rotationSmooth = 18f;
    public float followDamping = 9f;
    public float distanceDamping = 9f;

    [Header("Arm settings")]
    public float targetDistance = 4f;
    public float minDistance = 1.2f;
    public float wallPadding = 0.4f;
    public float cameraRadius = 0.25f;    // radio usado en SphereCast
    public LayerMask obstacleLayers;

    [Header("Pivot offset")]
    public Vector3 pivotLocalOffset = new Vector3(0f, 1.6f, 0f);

    private float yaw;
    private float pitch = 10f; // ángulo inicial, editable
    private float currentDistance;
    private float smoothedMX, smoothedMY;
    private float velocityFollow; // para SmoothDamp si quieres

    void Start()
    {
        currentDistance = targetDistance;

        if (target && cameraTransform)
        {
            // inicializar yaw con la proyección plana entre cámara y target
            Vector3 toCam = cameraTransform.position - target.position;
            if (toCam.sqrMagnitude > 0.0001f)
            {
                Vector3 flat = new Vector3(toCam.x, 0f, toCam.z);
                yaw = Mathf.Atan2(flat.x, flat.z) * Mathf.Rad2Deg;
            }
        }
    }

    void LateUpdate()
    {
        if (!target || !cameraTransform) return;

        // 1) Seguir al personaje (posición) con damping
        Vector3 desiredPivotPos = target.position + pivotLocalOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPivotPos, followDamping * Time.deltaTime);

        // 2) Leer ratón y suavizar entrada
        float rawMX = Input.GetAxis("Mouse X");
        float rawMY = Input.GetAxis("Mouse Y") * (invertY ? 1f : -1f);

        smoothedMX = Mathf.Lerp(smoothedMX, rawMX, inputSmooth * Time.deltaTime);
        smoothedMY = Mathf.Lerp(smoothedMY, rawMY, inputSmooth * Time.deltaTime);

        float appliedMX = smoothedMX * mouseSensitivity * sensitivityMultiplier * Time.deltaTime;
        float appliedMY = smoothedMY * mouseSensitivity * sensitivityMultiplier * Time.deltaTime;

        yaw += appliedMX;
        pitch += appliedMY;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        // 3) Aplicar yaw al pivot (solo Y axis). Esto evita que el pivot se incline.
        Quaternion targetYawRot = Quaternion.Euler(0f, yaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetYawRot, rotationSmooth * Time.deltaTime);

        // 4) Calculamos la rotación completa deseada de la cámara (pitch + yaw)
        Quaternion cameraWorldRot = Quaternion.Euler(pitch, yaw, 0f);

        // 5) Colisión: SphereCast desde el pivot hacia la posición deseada de la cámara
        Vector3 desiredDir = cameraWorldRot * Vector3.forward; // dirección hacia adelante de la cámara
        // la posición deseada está detrás del pivot: pivot - forward * targetDistance
        Vector3 rayOrigin = transform.position;
        Vector3 rayDir = -desiredDir; // queremos ir hacia atrás del pivot
        float maxCheck = targetDistance + wallPadding;

        float desiredDistance = targetDistance;
        if (Physics.SphereCast(rayOrigin, cameraRadius, rayDir, out RaycastHit hit, maxCheck, obstacleLayers, QueryTriggerInteraction.Ignore))
        {
            // hit.distance es la distancia desde el origen hasta el punto de impacto en la dirección rayDir
            float hitDist = Mathf.Max(hit.distance - wallPadding, minDistance);
            desiredDistance = Mathf.Clamp(hitDist, minDistance, targetDistance);
        }

        // 6) Suavizar la distancia y poner la posición final de la cámara en mundo
        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, distanceDamping * Time.deltaTime);
        Vector3 cameraWorldPos = transform.position + rayDir.normalized * currentDistance;
        cameraTransform.position = cameraWorldPos;

        // 7) Aplicar rotación a la cámara (en world space) para que mire según pitch+yaw y se garantice que se vea al target
        cameraTransform.rotation = cameraWorldRot;

        // 8) Opcional: asegurar que la cámara mira al target exactamente (útil si quieres que siempre centre)
        // cameraTransform.LookAt(target.position);
    }
}

