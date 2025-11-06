using UnityEngine;

public class Moneda : MonoBehaviour
{
    public int valor = 1;

    void OnTriggerEnter(Collider other)
    {
        PlayerMonedas jugador = other.GetComponent<PlayerMonedas>();
        if (jugador != null)
        {
            jugador.AñadirMoneda(valor);
            Destroy(gameObject);
        }
    }
}

