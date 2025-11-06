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

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerCoins = PlayerPrefs.GetInt("Monedas", 0); // Cargar monedas guardadas
        UpdateCoinUI();

        foreach (var item in shopItems)
        {
            item.Initialize();

            if (PlayerPrefs.GetInt(item.itemName + "_Comprado", 0) == 1)
            {
                item.isPurchased = true;
            }

            string equipped = PlayerPrefs.GetString("Cosmetico" + item.itemType.ToString(), "");
            item.SetEquipped(item.itemName == equipped);
        }
    }

    public void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = $"{playerCoins}";
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
        PlayerPrefs.SetString("Cosmetico" + type.ToString(), itemName);
        PlayerPrefs.Save();

        foreach (var item in shopItems)
        {
            if (item.itemType == type)
            {
                item.SetEquipped(item.itemName == itemName);
            }
        }

        if (cosmeticController != null)
        {
            cosmeticController.ActualizarCosmetico(type.ToString(), itemName);
        }

        Debug.Log($"Equipado: {itemName} en slot {type}");
    }
}

