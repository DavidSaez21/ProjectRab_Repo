using System.Collections;
using UnityEngine;
using TMPro;

public class Texto : MonoBehaviour
{
    [Header("Configuración")]
    public float tiempoEntreLetras = 0.05f; // Velocidad de escritura
    public TextMeshProUGUI textMeshProUGUI; // Referencia al componente de texto
    public string[] textos; // Lista de textos a mostrar

    private int textoIndex = 0;
    private bool escribiendo = false;
    private bool textoTerminado = false;

    void Start()
    {
        textMeshProUGUI.text = "";
        StartCoroutine(MostrarTexto());
    }

    void Update()
    {
        // Solo avanza si el texto terminó y se hace clic izquierdo
        if (textoTerminado && Input.GetMouseButtonDown(0))
        {
            textoTerminado = false;
            textMeshProUGUI.text = "";

            if (textoIndex < textos.Length)
            {
                StartCoroutine(MostrarTexto());
            }
            else
            {
                // Si ya no hay más textos, puedes ocultar el texto o hacer otra acción
                textMeshProUGUI.text = "";
                Debug.Log("Fin de los textos.");
            }
        }
    }

    IEnumerator MostrarTexto()
    {
        escribiendo = true;
        string textoActual = textos[textoIndex];
        textMeshProUGUI.text = "";

        foreach (char letra in textoActual)
        {
            textMeshProUGUI.text += letra;
            yield return new WaitForSeconds(tiempoEntreLetras);
        }

        escribiendo = false;
        textoTerminado = true;
        textoIndex++;
    }
}
