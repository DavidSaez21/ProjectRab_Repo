using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; // La pelota
    public Vector3 offset = new Vector3(0, 3, -4); // Ajusta según tu escena
    public float smoothSpeed = 5f;
    public LayerMask obstacleLayers; // Asigna en el Inspector las capas que bloquean la cámara

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 direction = desiredPosition - target.position;

        RaycastHit hit;
        Vector3 finalPosition = desiredPosition;

        // Detecta si hay una pared entre el jugador y la cámara
        if (Physics.Raycast(target.position, direction.normalized, out hit, offset.magnitude, obstacleLayers))
        {
            // Coloca la cámara justo antes del obstáculo
            finalPosition = hit.point - direction.normalized * 0.3f; // Ajuste para evitar que se pegue a la pared
        }

        // Suaviza el movimiento de la cámara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, finalPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Mira al jugador
        transform.LookAt(target);
    }
}

