using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscenaAlRecoger : MonoBehaviour
{
    public string SCN_Victory = "SCN_Victory";

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CargarSiguienteEscena();
        }
    }

    void CargarSiguienteEscena()
    {
        SceneManager.LoadScene(SCN_Victory);
    }
}
