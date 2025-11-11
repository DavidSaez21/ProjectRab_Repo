using UnityEngine;

public class OrbitSpringArmCamera : MonoBehaviour
{
    [Header("References")]
    public Transform target;
    public Transform cameraTransform;

    [Header("Orbit controls")]
    public float mouseSensitivity = 160f; // base
    [Range(0.05f, 1f)] public float sensitivityMultiplier = 0.1f; // nuevo multiplicador (baja para menos "dureza")
    public float pitchMin = -30f;
    public float pitchMax = 65f;
    public bool invertY = false;

    [Header("Smoothing")]
    public float inputSmooth = 10f;
    public float rotationSmooth = 18f;
    public float followDamping = 9f;
    public float distanceDamping = 9f;

    [Header("Arm settings")]
    public float targetDistance = 5f;
    public float minDistance = 1.2f;
    public float wallPadding = 0.4f;
    public LayerMask obstacleLayers;

    [Header("Pivot offset")]
    public Vector3 pivotLocalOffset = new Vector3(0f, 1.2f, 0f);

    private float yaw;
    private float pitch;
    private float currentDistance;
    private float smoothedMX, smoothedMY;

    void Start()
    {
        currentDistance = targetDistance;
        if (target && cameraTransform)
        {
            Vector3 toCam = (cameraTransform.position - target.position);
            if (toCam.sqrMagnitude > 0.0001f)
            {
                Vector3 flat = new Vector3(toCam.x, 0f, toCam.z);
                yaw = Mathf.Atan2(flat.x, flat.z) * Mathf.Rad2Deg;
                pitch = Mathf.Atan2(toCam.y, flat.magnitude) * Mathf.Rad2Deg;
                pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
            }
        }
    }

    void LateUpdate()
    {
        if (!target || !cameraTransform) return;

        // Pivot sigue la posición del target (solo posición, no rotación)
        Vector3 desiredPivotPos = target.position + pivotLocalOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPivotPos, followDamping * Time.deltaTime);

        // Lectura del ratón con suavizado
        float rawMX = Input.GetAxis("Mouse X");
        float rawMY = Input.GetAxis("Mouse Y") * (invertY ? 1f : -1f);

        smoothedMX = Mathf.Lerp(smoothedMX, rawMX, inputSmooth * Time.deltaTime);
        smoothedMY = Mathf.Lerp(smoothedMY, rawMY, inputSmooth * Time.deltaTime);

        // Aplicar sensibilidad y multiplicador (menos duro si sensitivityMultiplier < 1)
        float appliedMX = smoothedMX * mouseSensitivity * sensitivityMultiplier * Time.deltaTime;
        float appliedMY = smoothedMY * mouseSensitivity * sensitivityMultiplier * Time.deltaTime;

        yaw += appliedMX;
        pitch += appliedMY;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        // Rotación del pivot suavizada
        Quaternion targetRot = Quaternion.Euler(pitch, yaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSmooth * Time.deltaTime);

        // Brazo y colisión
        Vector3 dir = -transform.forward;
        float desiredDistance = targetDistance;
        float checkDistance = targetDistance + wallPadding;

        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, checkDistance, obstacleLayers, QueryTriggerInteraction.Ignore))
        {
            float hitDist = Mathf.Max(hit.distance - wallPadding, minDistance);
            desiredDistance = Mathf.Clamp(hitDist, minDistance, targetDistance);
        }

        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, distanceDamping * Time.deltaTime);

        cameraTransform.position = transform.position + dir * currentDistance;
        cameraTransform.LookAt(target.position);
    }
}
