using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName = "Objeto";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aquí puedes agregar lógica para añadir el objeto al inventario
            Debug.Log($"Has recogido: {itemName}");

            // Desactivar o destruir el objeto
            Destroy(gameObject);
        }
    }
}
