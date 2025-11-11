using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("Editor References")]
    public Rigidbody playerRb; //Referencia al Rigidbody del player
    public AudioSource playerAudio; //Ref al emisor de sonidos del player

    [Header("Movement Parameters")]
    public float speed = 10;
    public Vector2 moveInput; //Almacén del input de movimiento de los periféricos que usamos para jugar

    [Header("Jump Parameters")]
    public float jumpForce = 6;
    public bool isGrounded = true;

    [Header("Respawn System")]
    public float fallLimit = -10;
    public Transform respawnPoint;

    [Header("Sound Configuration")]
    public AudioClip[] soundCollection;

    [Header("Camera Reference")]
    public Transform cameraTransform; // Asigna la cámara en el Inspector


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //CinematicMovement();
        //Respawn por altura
        if (transform.position.y <= fallLimit)
        {
            Respawn();
        }
    }

    private void FixedUpdate()
    {
        //Update para calcular movimientos físicos
        PhysicalMovement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; //Devuelve la capacidad de saltar
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Respawn();
        }
    }


    void CinematicMovement()
    {
        //Movimiento = (Dirección * velocidad * input)
        //Necesitais multiplicar el movimiento por Time.deltaTime
        transform.Translate(Vector3.right * speed * moveInput.x * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * moveInput.y * Time.deltaTime);
    }

    void PhysicalMovement()
    {
        // Obtener dirección de la cámara
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // Ignorar componente vertical
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // Calcular dirección de movimiento
        Vector3 moveDirection = camForward * moveInput.y + camRight * moveInput.x;

        // Aplicar fuerza en esa dirección
        playerRb.AddForce(moveDirection * speed);
    }


    void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        PlaySFX(0);
    }

    void Respawn()
    {
        //Sustituir el transform.position del player por el del punto de respawn
        transform.position = respawnPoint.position;
        //Resetear el valor de aceleración del rigidbody
        playerRb.linearVelocity = new Vector3(0,0,0);
        PlaySFX(2);
    }

    public Transform cosmeticsContainer;

    void LateUpdate()
    {
        // Obtener la rotación Y de la cámara
        Vector3 cameraEuler = cameraTransform.eulerAngles;

        // Aplicar solo la rotación Y al contenedor
        cosmeticsContainer.rotation = Quaternion.Euler(0, cameraEuler.y, 0);
    }



    public void PlaySFX(int soundToPlay)
    {
        playerAudio.PlayOneShot(soundCollection[soundToPlay]);
    }

    #region Input Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) 
    {

        if (context.performed && isGrounded == true)
        {
            isGrounded = false;
            Jump();
        }
    }




    #endregion
}
