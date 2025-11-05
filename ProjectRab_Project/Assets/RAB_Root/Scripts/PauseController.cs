using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel; // Asigna el panel en el Inspector

    private bool menuAbierto = false;

    public void CerrarMenu()
    {
        menuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        menuAbierto = false;
    }

    public class PauseController : MonoBehaviour
    {
        public GameObject menuPanel;
        private bool menuAbierto = false;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuAbierto = !menuAbierto;
                menuPanel.SetActive(menuAbierto);

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
        }

        public void CerrarMenu()
        {
            menuPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            menuAbierto = false;
        }



        public class MenuManager : MonoBehaviour
        {
            public GameObject menuPanel;
            private bool menuAbierto = false;

            void Update()
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    menuAbierto = !menuAbierto;
                    menuPanel.SetActive(menuAbierto);

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
            }

            public void CerrarMenu()
            {
                menuPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                menuAbierto = false;
            }

            // ✅ Este método Unity lo detectará seguro
            public void CargarMenuPrincipal()
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("SCN_MainMenu");
            }
        }


        public void IrAlMenuPrincipal()
        {
            Time.timeScale = 1f; // Asegúrate de reanudar el tiempo
            SceneManager.LoadScene("SCN_MainMenu"); // Usa el nombre exacto de la escena
        }
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuAbierto = !menuAbierto;
            menuPanel.SetActive(menuAbierto);

            if (menuAbierto)
            {
                Cursor.lockState = CursorLockMode.None; // Libera el cursor
                Cursor.visible = true; // Lo hace visible
                Time.timeScale = 0f; // Pausa el juego
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked; // Oculta el cursor
                Cursor.visible = false;
                Time.timeScale = 1f; // Reanuda el juego
            }
        }
    }

}
