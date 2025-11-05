using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    public int playerCoins = 100;
    public List<ShopItem> shopItems;
    public TMP_Text coinText;

    public CosmeticController cosmeticController; // Asignar en el Inspector



    void Awake()
    {
        Instance = this;
    }

    void Start()

    {
        UpdateCoinUI();

        foreach (var item in shopItems)
        {
            item.Initialize();

            // Marcar como comprado si ya lo está
            if (PlayerPrefs.GetInt(item.itemName + "_Comprado", 0) == 1)
            {
                item.isPurchased = true;
            }

            // Marcar como equipado si coincide con lo guardado
            string equipped = PlayerPrefs.GetString("Cosmetico" + item.itemType.ToString(), "");
            item.SetEquipped(item.itemName == equipped);
        }

    }



    public void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = $"{playerCoins}";
    }

    public bool TryPurchaseItem(string itemName, int price)
    {
        if (playerCoins >= price)
        {
            playerCoins -= price;
            PlayerPrefs.SetInt(itemName + "_Comprado", 1);
            PlayerPrefs.Save();
            UpdateCoinUI();
            return true;
        }
        return false;
    }

    public void EquipItem(string itemName, ShopItem.ItemType type)
    {
        // Guardar en PlayerPrefs
        PlayerPrefs.SetString("Cosmetico" + type.ToString(), itemName);
        PlayerPrefs.Save();

        // Actualizar visualmente en la tienda
        foreach (var item in shopItems)
        {
            if (item.itemType == type)
            {
                item.SetEquipped(item.itemName == itemName);
            }
        }

        // Activar el objeto en el personaje
        if (cosmeticController != null)
        {
            cosmeticController.ActualizarCosmetico(type.ToString(), itemName);
        }

        Debug.Log($"Equipado: {itemName} en slot {type}");
    }
}

