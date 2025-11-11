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
        // Uncomment if quieres que el ShopManager sobreviva entre escenas
        // DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        playerCoins = PlayerPrefs.GetInt("Monedas", 0);
        UpdateCoinUI();

        // Inicializar items y cargar comprado/equipped desde la misma clave consistente
        foreach (var item in shopItems)
        {
            item.Initialize();

            // comprado
            if (PlayerPrefs.GetInt(item.itemName + "_Comprado", 0) == 1)
                item.isPurchased = true;

            // Determinar la clave de categoría consistente (misma que EquipItem)
            string categoriaClave = item.itemType switch
            {
                ShopItem.ItemType.capas => "Capa",
                ShopItem.ItemType.cascos => "Cabeza",
                ShopItem.ItemType.escudos => "Escudo",
                ShopItem.ItemType.espadas => "Espada",
                _ => ""
            };

            // comprobar qué está equipado en PlayerPrefs usando la misma clave
            if (!string.IsNullOrEmpty(categoriaClave))
            {
                string equipado = PlayerPrefs.GetString("Cosmetico" + categoriaClave, "");
                item.SetEquipped(item.itemName == equipado);
            }
        }

        // Después de haber marcado los ShopItems, actualizar visuales del personaje
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

        // Guardar qué cosmético está equipado
        PlayerPrefs.SetString("Cosmetico" + categoriaClave, itemName);
        PlayerPrefs.Save();

        // Actualizar estado de los ShopItem en memoria
        foreach (var item in shopItems)
        {
            if (item.itemType == type)
                item.SetEquipped(item.itemName == itemName);
        }

        // Actualizar visuales del personaje
        if (cosmeticController != null)
            cosmeticController.ActualizarCosmetico(categoriaClave, itemName);

        Debug.Log($"Equipado: {itemName} en slot {categoriaClave}");
    }
}

