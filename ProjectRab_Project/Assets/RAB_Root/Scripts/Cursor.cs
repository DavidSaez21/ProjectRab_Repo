using UnityEngine;

// Si usas el nuevo sistema Input System
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
using UnityEngine.InputSystem;
#endif

public class FollowMouseUI : MonoBehaviour
{
    [Header("Referencia al objeto de UI que será el cursor")]
    public RectTransform uiCursor;

    [Header("Offset (ajusta la punta del cursor)")]
    public Vector2 hotspot = Vector2.zero;

    [Header("Velocidad de seguimiento (opcional)")]
    public float followSpeed = 40f;

    void Start()
    {
        if (uiCursor == null)
            uiCursor = GetComponent<RectTransform>();

        Cursor.visible = false;  // Oculta el cursor del sistema
        Cursor.lockState = CursorLockMode.None; // No bloquea movimiento
    }

    void Update()
    {
        Vector2 mousePos = Vector2.zero;

        // Detectar automáticamente el sistema de input activo
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        if (Mouse.current != null)
            mousePos = Mouse.current.position.ReadValue();
#else
        mousePos = Input.mousePosition;
#endif

        // Mover el cursor de UI
        uiCursor.position = Vector2.Lerp(uiCursor.position, mousePos + hotspot, Time.deltaTime * followSpeed);
    }

    void OnDisable()
    {
        Cursor.visible = true;
    }
}