using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    [Header("Objetos que deben ocultarse al abrir el panel")]
    public GameObject[] objetosAOcultar;

    [Header("Objetos que deben mostrarse al abrir el panel")]
    public GameObject[] objetosAMostrar;

    private bool isPanelOpen = false;

    // Llamado al pulsar el botón que abre el panel
    public void AbrirPanel()
    {
        isPanelOpen = true;
        ActualizarUI();
    }

    // Llamado por el botón Return dentro del panel
    public void CerrarPanel()
    {
        isPanelOpen = false;
        ActualizarUI();
    }

    private void ActualizarUI()
    {
        foreach (GameObject obj in objetosAOcultar)
        {
            obj.SetActive(!isPanelOpen);
        }

        foreach (GameObject obj in objetosAMostrar)
        {
            obj.SetActive(isPanelOpen);
        }
    }
}
