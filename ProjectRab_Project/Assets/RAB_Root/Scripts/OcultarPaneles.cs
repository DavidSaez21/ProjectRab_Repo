using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel; // El panel que se va a mostrar
    public GameObject elementosInferiores; // El contenedor de los elementos que quieres ocultar

    public void AbrirPanel()
    {
        panel.SetActive(true);
        elementosInferiores.SetActive(false);
    }

    public void CerrarPanel()
    {
        panel.SetActive(false);
        elementosInferiores.SetActive(true);
    }
}

