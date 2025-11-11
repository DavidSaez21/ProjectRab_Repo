using UnityEngine;

public class Moneda : MonoBehaviour
{
    public int valor = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ShopManager.Instance != null)
            {
                ShopManager.Instance.AñadirMonedas(valor);
            }
            Destroy(gameObject);
        }
    }
}
