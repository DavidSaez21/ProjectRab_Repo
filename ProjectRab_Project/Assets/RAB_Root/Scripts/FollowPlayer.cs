using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;       // La pelota
    public Vector3 offset = new Vector3(0, 3, -4); // Ajusta según tu escena
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
