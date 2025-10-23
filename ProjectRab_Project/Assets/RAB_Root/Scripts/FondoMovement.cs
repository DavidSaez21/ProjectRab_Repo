using UnityEngine;
using UnityEngine.UI;

public class ScrollRawImage : MonoBehaviour
{
    public float velocidad = 0.2f; // unidades por segundo
    public bool horizontal = true;
    public bool vertical = false;

    private RawImage rawImage;
    private Rect uv;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        uv = rawImage.uvRect;
    }

    void Update()
    {
        float delta = velocidad * Time.deltaTime;

        if (horizontal) uv.x += delta;
        if (vertical) uv.y += delta;

        // Mantener el valor entre 0 y 1 para evitar números grandes
        if (uv.x > 1f || uv.x < -1f) uv.x = uv.x % 1f;
        if (uv.y > 1f || uv.y < -1f) uv.y = uv.y % 1f;

        rawImage.uvRect = uv;
    }
}
