using TMPro;
using UnityEngine;
using System.Collections;

public class TextoProgresivo : MonoBehaviour
{
    public float delay = 0.05f; // Tiempo entre letras
    public string fullText;     // El texto completo que quieres mostrar
    private string currentText = "";
    private TMP_Text textMeshPro; // Referencia al componente TextMeshPro

    void Start()
    {
        textMeshPro = GetComponent<TMP_Text>();
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textMeshPro.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
