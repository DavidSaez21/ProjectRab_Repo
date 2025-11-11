using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public GameObject notePanel; // Panel UI con la nota
    public Button continueButton;
    public MonoBehaviour playerController; // el script que gestiona entrada/movimiento del jugador

    void Start()
    {
        // Mostrar nota al inicio
        ShowNote();
        // Conectar el botón
        continueButton.onClick.AddListener(HideNote);
    }

    void ShowNote()
    {
        notePanel.SetActive(true);
        // Pausa el juego (detiene Time.deltaTime based systems)
        Time.timeScale = 0f;
        // Desactivar controlador del jugador para evitar input
        if (playerController != null) playerController.enabled = false;
        // Opcional: desbloquear cursor si tu juego lo bloquea
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HideNote()
    {
        notePanel.SetActive(false);
        // Reanuda el juego
        Time.timeScale = 1f;
        // Reactivar controlador del jugador
        if (playerController != null) playerController.enabled = true;
        // Opcional: volver a bloquear cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
