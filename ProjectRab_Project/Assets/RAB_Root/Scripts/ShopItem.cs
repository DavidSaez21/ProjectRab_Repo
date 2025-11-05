using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public string itemName;
    public int price;
    public Button actionButton;
    public TMP_Text buttonText;
    public GameObject tickIcon;

    public enum ItemType { Sword, Shield, Cape, Helmet }
    public ItemType itemType;

    public TMP_Text usingText;

    public bool isPurchased = false; // ← Ahora es pública
    private bool isEquipped = false;

    public void Initialize()
    {
        UpdateUI();
        actionButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (!isPurchased)
        {
            if (ShopManager.Instance.TryPurchaseItem(itemName, price))
            {
                isPurchased = true;
                UpdateUI();
            }
        }
        else
        {
            EquipItem();
        }
    }

    void EquipItem()
    {
        ShopManager.Instance.EquipItem(itemName, itemType);
        isEquipped = true;
        UpdateUI();
    }

    public void UpdateUI()
    {
        tickIcon.SetActive(isPurchased);
        buttonText.text = isPurchased ? "Equip" : $"Buy ({price})";
    }

    public void SetEquipped(bool equipped)
    {
        isEquipped = equipped;
        if (usingText != null)
            usingText.gameObject.SetActive(equipped);
    }
}

