using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("Follow settings")]
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -4);
    public float smoothSpeed = 10f;

    [Header("Collision settings")]
    public LayerMask obstacleLayers;
    public float sphereRadius = 0.6f;
    public float minDistance = 1.2f;
    public float wallPadding = 0.5f;
    public float castOriginHeight = 0.5f; // ligeramente por encima del suelo

    void LateUpdate()
    {
        if (!target) return;

        // Posición deseada detrás/encima del jugador
        Vector3 desiredPosition = target.position + offset;
        Vector3 toCam = desiredPosition - target.position;
        Vector3 dir = toCam.sqrMagnitude > 0.0001f ? toCam.normalized : Vector3.back;

        // Origen del cast ligeramente elevado
        Vector3 castOrigin = target.position + Vector3.up * castOriginHeight;

        Vector3 finalPosition = desiredPosition;
        RaycastHit hit;

        // 1) Obstáculo entre el jugador y la cámara (spherecast)
        if (Physics.SphereCast(castOrigin, sphereRadius, dir, out hit, toCam.magnitude, obstacleLayers, QueryTriggerInteraction.Ignore))
        {
            // Coloca la cámara justo antes del obstáculo y separa por la normal
            Vector3 contact = hit.point - hit.normal * wallPadding;

            // Proyecta hacia la dirección general para mantener encuadre
            Vector3 projected = Vector3.Project(contact - target.position, dir);
            finalPosition = target.position + projected;
        }

        // 2) Si la cámara quedó "dentro" de un collider, empuja hacia fuera
        if (Physics.Linecast(finalPosition, target.position, out hit, obstacleLayers, QueryTriggerInteraction.Ignore))
        {
            finalPosition = hit.point + hit.normal * wallPadding;
        }

        // 3) En ningún caso más cerca que minDistance del jugador
        float dist = Vector3.Distance(target.position, finalPosition);
        if (dist < minDistance)
        {
            finalPosition = target.position - dir * minDistance;
        }

        // 4) Suavizado del movimiento
        Vector3 smoothed = Vector3.Lerp(transform.position, finalPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothed;

        // 5) Mirar al jugador
        transform.LookAt(target);
    }
}
