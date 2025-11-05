using UnityEngine;
using UnityEngine.UI;

public class CosmeticItemUI : MonoBehaviour
{
    public string itemID;           // Ej: "Espada01"
    public string category;         // Ej: "Espada"
    public Button actionButton;     // Botón de Comprar/Equipar
    public Text buttonText;         // Texto del botón
    public GameObject tickImage;    // Tick verde visual

    void Start()
    {
        UpdateButtonState();
    }

    public void OnButtonClick()
    {
        if (!IsPurchased())
        {
            PurchaseItem();
        }
        else
        {
            EquipItem();
        }

        UpdateButtonState();
    }

    void PurchaseItem()
    {
        PlayerPrefs.SetInt(itemID + "_Purchased", 1);
        PlayerPrefs.Save();
    }

    void EquipItem()
    {
        // Desactiva cualquier otro equipado en la misma categoría
        foreach (string id in GetAllItemsInCategory())
        {
            PlayerPrefs.SetInt(id + "_Equipped", 0);
        }

        PlayerPrefs.SetInt(itemID + "_Equipped", 1);
        PlayerPrefs.Save();
    }

    void UpdateButtonState()
    {
        bool purchased = IsPurchased();
        bool equipped = IsEquipped();

        if (!purchased)
        {
            buttonText.text = "Comprar";
            tickImage.SetActive(false);
        }
        else
        {
            buttonText.text = equipped ? "Equipado" : "Equipar";
            tickImage.SetActive(true);
        }
    }

    bool IsPurchased()
    {
        return PlayerPrefs.GetInt(itemID + "_Purchased", 0) == 1;
    }

    bool IsEquipped()
    {
        return PlayerPrefs.GetInt(itemID + "_Equipped", 0) == 1;
    }

    string[] GetAllItemsInCategory()
    {
        // Puedes reemplazar esto con una lista dinámica si lo prefieres
        switch (category)
        {
            case "Espada": return new string[] { "Espada01", "Espada02" };
            case "Casco": return new string[] { "Casco01", "Casco02" };
            case "Escudo": return new string[] { "Escudo01", "Escudo02" };
            case "Capa": return new string[] { "Capa01", "Capa02" };
            default: return new string[0];
        }
    }
}
