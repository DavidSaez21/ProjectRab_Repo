using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panel del menú (asignar en inspector)")]
    public GameObject menuPanel;

    private bool menuAbierto = false;

    void Start()
    {
        // Asegurar estado inicial: menú cerrado, cursor bloqueado
        if (menuPanel != null) menuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        // Alternar menú con Escape
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        menuAbierto = !menuAbierto;
        if (menuPanel != null) menuPanel.SetActive(menuAbierto);

        if (menuAbierto)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }

    // Método público que puedes vincular al botón "Cerrar" del UI
    public void CerrarMenu()
    {
        menuAbierto = false;
        if (menuPanel != null) menuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    // Método público que puedes vincular al botón "Ir al menú principal"
    public void CargarMenuPrincipal()
    {
        Time.timeScale = 1f; // Asegurar tiempo normal antes de cargar escena
        SceneManager.LoadScene("SCN_MainMenu");
    }
}

