using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -4);
    public float smoothSpeed = 5f;
    public LayerMask obstacleLayers;

    public float sphereRadius = 0.3f;   // radio para evitar atravesar esquinas
    public float minDistance = 1.0f;    // distancia mínima cámara-jugador

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 direction = desiredPosition - target.position;

        RaycastHit hit;
        Vector3 finalPosition = desiredPosition;

        // Detecta obstáculos con SphereCast
        if (Physics.SphereCast(target.position, sphereRadius, direction.normalized, out hit, offset.magnitude, obstacleLayers))
        {
            finalPosition = hit.point - direction.normalized * 0.3f;
        }

        // Evitar que la cámara se acerque demasiado al jugador
        float distance = Vector3.Distance(target.position, finalPosition);
        if (distance < minDistance)
        {
            finalPosition = target.position - direction.normalized * minDistance;
        }

        // Suavizar movimiento
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, finalPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Mirar al jugador
        transform.LookAt(target);
    }
}
