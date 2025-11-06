using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupChangeScene : MonoBehaviour
{
    public string SCN_Victory; 

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SCN_Victory);
        }
    }
}