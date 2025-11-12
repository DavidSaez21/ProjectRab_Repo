using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [SerializeField] private int playerCoins;
    public int PlayerCoins => playerCoins;

    public List<ShopItem> shopItems;
    public TMP_Text coinText;
    public CosmeticController cosmeticController;

    public event Action<int> OnCoinsChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Mantén la instancia entre escenas si lo deseas

        // Cargar monedas en Awake para que estén disponibles inmediatamente
        playerCoins = PlayerPrefs.GetInt("Monedas", 0);
        UpdateCoinUI();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Primer arranque por versión: si no existe la marca, inicializa a 0
        string primeraClave = "HasRun_v" + Application.version;
        if (PlayerPrefs.GetInt(primeraClave, 0) == 0)
        {
            PlayerPrefs.SetInt("Monedas", 0);
            PlayerPrefs.SetInt(primeraClave, 1);
            PlayerPrefs.Save();
            Debug.Log("Primera ejecución de la versión: monedas reiniciadas a 0");
        }

        // Cargar monedas (ahora ya seguro que será 0 en la primera ejecución)
        playerCoins = PlayerPrefs.GetInt("Monedas", 0);
        UpdateCoinUI();

    }

    void Start()
    {
        // Inicializar items y estados guardados
        foreach (var item in shopItems)
        {
            item.Initialize();

            if (PlayerPrefs.GetInt(item.itemName + "_Comprado", 0) == 1)
                item.isPurchased = true;

            string categoriaClave = item.itemType switch
            {
                ShopItem.ItemType.capas => "Capa",
                ShopItem.ItemType.cascos => "Cabeza",
                ShopItem.ItemType.escudos => "Escudo",
                ShopItem.ItemType.espadas => "Espada",
                _ => ""
            };

            if (!string.IsNullOrEmpty(categoriaClave))
            {
                string equipado = PlayerPrefs.GetString("Cosmetico" + categoriaClave, "");
                item.SetEquipped(item.itemName == equipado);
            }
        }

        if (cosmeticController != null)
        {
            cosmeticController.ActualizarCosmetico("Capa", PlayerPrefs.GetString("CosmeticoCapa", ""));
            cosmeticController.ActualizarCosmetico("Cabeza", PlayerPrefs.GetString("CosmeticoCabeza", ""));
            cosmeticController.ActualizarCosmetico("Escudo", PlayerPrefs.GetString("CosmeticoEscudo", ""));
            cosmeticController.ActualizarCosmetico("Espada", PlayerPrefs.GetString("CosmeticoEspada", ""));
        }
    }

    public void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = $"{playerCoins}";
        OnCoinsChanged?.Invoke(playerCoins);
    }

    public void AñadirMonedas(int cantidad)
    {
        playerCoins += cantidad;
        PlayerPrefs.SetInt("Monedas", playerCoins);
        PlayerPrefs.Save();
        Debug.Log("Monedas actuales: " + playerCoins);
        UpdateCoinUI();
    }

    public bool TryPurchaseItem(string itemName, int price)
    {
        if (playerCoins >= price)
        {
            playerCoins -= price;
            PlayerPrefs.SetInt("Monedas", playerCoins);
            PlayerPrefs.SetInt(itemName + "_Comprado", 1);
            PlayerPrefs.Save();
            UpdateCoinUI();
            return true;
        }
        return false;
    }

    public void EquipItem(string itemName, ShopItem.ItemType type)
    {
        string categoriaClave = type switch
        {
            ShopItem.ItemType.capas => "Capa",
            ShopItem.ItemType.cascos => "Cabeza",
            ShopItem.ItemType.escudos => "Escudo",
            ShopItem.ItemType.espadas => "Espada",
            _ => ""
        };

        if (string.IsNullOrEmpty(categoriaClave)) return;

        PlayerPrefs.SetString("Cosmetico" + categoriaClave, itemName);
        PlayerPrefs.Save();

        foreach (var item in shopItems)
        {
            if (item.itemType == type)
                item.SetEquipped(item.itemName == itemName);
        }

        if (cosmeticController != null)
            cosmeticController.ActualizarCosmetico(categoriaClave, itemName);

        Debug.Log($"Equipado: {itemName} en slot {categoriaClave}");
    }
}

