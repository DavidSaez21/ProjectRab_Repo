using UnityEngine;
using System.Collections.Generic;

public class CosmeticController : MonoBehaviour
{
    public List<GameObject> capas;
    public List<GameObject> cascos;
    public List<GameObject> escudos;
    public List<GameObject> espadas;

    public void ActualizarCosmetico(string tipo, string nombre)
    {
        List<GameObject> lista = ObtenerListaPorTipo(tipo);

        foreach (GameObject obj in lista)
        {
            obj.SetActive(obj.name == nombre);
        }
    }

    private List<GameObject> ObtenerListaPorTipo(string tipo)
    {
        switch (tipo)
        {
            case "Capa": return capas;
            case "Cabeza": return cascos;
            case "Escudo": return escudos;
            case "Espada": return espadas;
            default: return new List<GameObject>();
        }
    }

    void Start()
    {
        DesactivarTodo();
        ActualizarCosmetico("Capa", PlayerPrefs.GetString("CosmeticoCapa", ""));
        ActualizarCosmetico("Cabeza", PlayerPrefs.GetString("CosmeticoCabeza", ""));
        ActualizarCosmetico("Escudo", PlayerPrefs.GetString("CosmeticoEscudo", ""));
        ActualizarCosmetico("Espada", PlayerPrefs.GetString("CosmeticoEspada", ""));
    }

    void DesactivarTodo()
    {
        DesactivarLista(capas);
        DesactivarLista(cascos);
        DesactivarLista(escudos);
        DesactivarLista(espadas);
    }

    void DesactivarLista(List<GameObject> lista)
    {
        foreach (GameObject obj in lista)
        {
            obj.SetActive(false);
        }
    }

}
