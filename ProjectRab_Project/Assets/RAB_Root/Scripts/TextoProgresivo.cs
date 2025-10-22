using TMPro;
using UnityEngine;
using System.Collections;

public class TextoProgresivo : MonoBehaviour
{
    public float delay = 0.05f; // Tiempo entre letras
    public TMP_Text textMeshPro;

    [TextArea(3, 10)]
    public string[] textos; // Lista de textos que aparecerán uno tras otro

    void Start()
    {
        textMeshPro = GetComponent<TMP_Text>();
        StartCoroutine(MostrarTextos());
    }

    IEnumerator MostrarTextos()
    {
        foreach (string fullText in textos)
        {
            textMeshPro.text = "";
            for (int i = 0; i <= fullText.Length; i++)
            {
                textMeshPro.text = fullText.Substring(0, i);
                yield return new WaitForSeconds(delay);
            }

            // Espera hasta que el jugador presione cualquier tecla
            yield return new WaitForSeconds(10f);
        }
    }
}
