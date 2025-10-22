using UnityEngine;
using UnityEngine.UI;

public class SimpleVolume : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider slider;

    void Start()
    {
        slider.value = audioSource.volume;
    }

    public void ChangeVolume(float value)
    {
        audioSource.volume = value;
    }
}