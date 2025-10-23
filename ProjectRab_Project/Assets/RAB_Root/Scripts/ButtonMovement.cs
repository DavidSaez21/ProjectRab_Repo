using UnityEngine;

public class BalanceoCinta : MonoBehaviour
{
    public float velocidad = 1f;  // Qué tan rápido se balancea
    public float amplitud = 5f;   // Ángulo máximo de rotación
    private float fase;

    void Start()
    {
        fase = Random.Range(0f, Mathf.PI * 2); // Para que cada cinta esté desfasada
    }

    void Update()
    {
        float angulo = Mathf.Sin(Time.time * velocidad + fase) * amplitud;
        transform.localRotation = Quaternion.Euler(0, 0, angulo);
    }
}