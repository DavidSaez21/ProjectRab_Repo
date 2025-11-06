using UnityEngine;
using UnityEngine.UI;

public class PlayerMonedas : MonoBehaviour
{
    public int monedas = 0;
    public Text textoMonedas; // Asigna el Text desde el Inspector

    public void AñadirMoneda(int cantidad)
    {
        monedas += cantidad;
        textoMonedas.text = "Monedas: " + monedas;
    }
}
