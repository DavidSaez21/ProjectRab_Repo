using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer;   // Asigna tu AudioMixer en el Inspector
    public Slider volumeSlider;     // Asigna el Slider en el Inspector

    private const string volumeKey = "GameVolume"; // clave para PlayerPrefs

    void Start()
    {
        // Cargar volumen guardado (si existe), si no, usar 1 (100%)
        float savedVolume = PlayerPrefs.GetFloat(volumeKey, 1f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        // Suscribir al evento de cambio
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        // Convertimos el valor [0,1] a decibelios (-80 dB a 0 dB aprox.)
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("Master", dB);

        // Guardamos el valor para la próxima vez
        PlayerPrefs.SetFloat(volumeKey, value);
    }
}
