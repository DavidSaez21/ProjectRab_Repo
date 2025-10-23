using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleMultiplier = 1.1f; // Tamaño al hacer hover
    public float speed = 10f; // Velocidad de transición

    private Vector3 originalScale;
    private Vector3 targetScale;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    void Update()
    {
        // Interpolamos suavemente entre el tamaño actual y el deseado
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * scaleMultiplier; // Aumenta tamaño
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale; // Vuelve al tamaño normal
    }
}