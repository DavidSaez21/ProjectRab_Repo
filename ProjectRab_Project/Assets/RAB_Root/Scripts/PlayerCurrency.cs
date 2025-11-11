using TMPro;
using UnityEngine;

public class PlayerMonedas : MonoBehaviour
{
    public TMP_Text textoMonedas;

    void Start()
    {
        if (ShopManager.Instance != null)
        {
            ShopManager.Instance.OnCoinsChanged += OnCoinsChanged;
            ActualizarUI(ShopManager.Instance.PlayerCoins);
        }
    }

    void OnDestroy()
    {
        if (ShopManager.Instance != null)
            ShopManager.Instance.OnCoinsChanged -= OnCoinsChanged;
    }

    void OnCoinsChanged(int newAmount)
    {
        ActualizarUI(newAmount);
    }

    public void ActualizarUI(int amount)
    {
        if (textoMonedas != null)
            textoMonedas.text = "Monedas: " + amount;
    }
}

