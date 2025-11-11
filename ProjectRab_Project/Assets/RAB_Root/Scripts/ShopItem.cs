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

    public enum ItemType { espadas, escudos, capas, cascos }
    public ItemType itemType;

    public TMP_Text usingText;

    public bool isPurchased = false;
    private bool isEquipped = false;

    bool isInitialized = false;

    public void Initialize()
    {
        if (isInitialized) return;
        isInitialized = true;

        UpdateUI();

        if (actionButton != null)
        {
            actionButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogWarning($"ActionButton no asignado en {name}");
        }

        Debug.Log($"UpdateUI en {name} | isPurchased={isPurchased} | isEquipped={isEquipped} | tickIconAssigned={(tickIcon != null)}");

        if (tickIcon != null)
        {
            tickIcon.SetActive(isPurchased);
            Debug.Log($"tickIcon activeSelf after set = {tickIcon.activeSelf} (tickIcon path: {GetHierarchyPath(tickIcon.transform)})");
        }

        if (buttonText != null)
            buttonText.text = isPurchased ? "Equip" : $"Buy ({price})";
    }

    string GetHierarchyPath(Transform t)
    {
        string path = t.name;
        while (t.parent != null)
        {
            t = t.parent;
            path = t.name + "/" + path;
        }
        return path;

    }





    void OnDestroy()
    {
        if (actionButton != null)
            actionButton.onClick.RemoveListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (ShopManager.Instance == null)
        {
            Debug.LogWarning("ShopManager.Instance es null al intentar comprar/equipar");
            return;
        }

        if (!isPurchased)
        {
            bool bought = ShopManager.Instance.TryPurchaseItem(itemName, price);
            if (bought)
            {
                isPurchased = true;
                UpdateUI();
            }
            else
            {
                // Opcional: feedback al usuario
                Debug.Log("No tienes suficientes monedas para " + itemName);
            }
        }
        else
        {
            EquipItem();
        }
    }

    void EquipItem()
    {
        if (ShopManager.Instance == null) return;

        ShopManager.Instance.EquipItem(itemName, itemType);
        // No establecemos isEquipped = true aquí porque SetEquipped será llamado desde ShopManager
        // pero dejamos esto por compatibilidad en caso de que quieras respuesta instantánea:
        // isEquipped = true;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (tickIcon != null)
            tickIcon.SetActive(isPurchased);

        if (usingText != null)
            usingText.gameObject.SetActive(isEquipped);

        if (buttonText != null && actionButton != null)
        {
            if (!isPurchased)
            {
                buttonText.text = $"Buy ({price})";
                actionButton.interactable = true;
            }
            else
            {
                // Prioriza estado equipado
                if (isEquipped)
                {
                    buttonText.text = "Using";
                    actionButton.interactable = true;
                }
                else
                {
                    buttonText.text = "Equip";
                    actionButton.interactable = true;
                }
            }
        }
    }

    public void SetEquipped(bool equipped)
    {
        isEquipped = equipped;
        if (usingText != null)
            usingText.gameObject.SetActive(equipped);
        Debug.Log($"{itemName} SetEquipped = {equipped}");
        UpdateUI();
    }

}
