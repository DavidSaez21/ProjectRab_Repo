using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public string itemName;
    public int price;

    public void OnClickBuy()
    {
        if (ShopManager.Instance == null) return;

        bool success = ShopManager.Instance.TryPurchaseItem(itemName, price);
        if (success)
        {
            Debug.Log("Compra realizada: " + itemName);
            // Aquí marcar UI del item como comprado (p. ej. llamar a un método del ShopItem)
        }
        else
        {
            Debug.Log("No tienes suficientes monedas");
            // Mostrar mensaje al usuario
        }
    }
}
