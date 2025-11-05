using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    public int playerCoins = 100;
    public List<ShopItem> shopItems;

    public TMP_Text coinText;

    public void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = $"{playerCoins}";
    }


    private string equippedItem = "";

    void Awake()
    {
        Instance = this;
    }



    void Start()
    {
        foreach (var item in shopItems)
        {
            item.Initialize();
        }
        {
            UpdateCoinUI();

            foreach (var item in shopItems)
            {
                item.Initialize();
            }
        }

    }





    public bool TryPurchaseItem(int price)
    {
        if (playerCoins >= price)
        {
            playerCoins -= price;
            UpdateCoinUI(); // Asegúrate de tener esta función definida
            return true;
        }
        return false;
    }


    public void EquipItem(string itemName)
    {
        equippedItem = itemName;
        foreach (var item in shopItems)
        {
            item.SetEquipped(item.itemName == itemName);
        }
        Debug.Log($"Equipped: {itemName}");
    }
    public void EquipItem(string itemName, ShopItem.ItemType type)
    {
        foreach (var item in shopItems)
        {
            if (item.itemType == type)
            {
                item.SetEquipped(item.itemName == itemName);
            }
        }
    }

}
