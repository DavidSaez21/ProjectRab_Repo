using UnityEngine;
using UnityEngine.UI;

public class PlayerMonedas : MonoBehaviour
{
    public Text textoMonedas; // Asigna el Text desde el Inspector

    void Start()
    {
        ActualizarUI();
    }

    public void ActualizarUI()
    {
        if (ShopManager.Instance != null)
        {
            textoMonedas.text = "Monedas: " + ShopManager.Instance.PlayerCoins;
        }
    }
}
