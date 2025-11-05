using UnityEngine;

public class MouseCameraController : MonoBehaviour
{
    public Transform target; // La pelota
    public Transform cameraTransform; // La cámara
    public float distance = 4f;
    public float height = 2f;
    public float sensitivity = 3f;
    public float verticalLimit = 80f;

    private float yaw = 0f;
    private float pitch = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Oculta y bloquea el cursor
    }

    void Update()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, -verticalLimit, verticalLimit);

        // Aplica rotación al pivot
        transform.position = target.position;
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Posiciona la cámara detrás del pivot
        cameraTransform.position = transform.position - transform.forward * distance + Vector3.up * height;
        cameraTransform.LookAt(target);
    }
}

