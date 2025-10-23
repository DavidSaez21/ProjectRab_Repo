using UnityEngine;
using TMPro;

public class TitleShine : MonoBehaviour
{
    public TMP_Text text;
    public Gradient gradient;
    public float speed = 0.5f;
    private float offset;

    void Update()
    {
        offset += Time.deltaTime * speed;
        var vertexColors = new VertexGradient(
            gradient.Evaluate((offset + 0f) % 1f),
            gradient.Evaluate((offset + 0.33f) % 1f),
            gradient.Evaluate((offset + 0.66f) % 1f),
            gradient.Evaluate((offset + 1f) % 1f)
        );
        text.colorGradient = vertexColors;
    }
}