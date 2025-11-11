using UnityEngine;
using UnityEngine.Video;   // Necesario para VideoPlayer y VideoClip
using UnityEngine.SceneManagement;

public class VideoIntro : MonoBehaviour
{
    [Header("Asignar en el Inspector")]
    public VideoPlayer videoPlayer;   // El componente VideoPlayer en tu objeto
    public VideoClip miVideo;         // El archivo de video que arrastras aquí
    public string nombreEscena;       // La escena a la que quieres saltar

    void Start()
    {
        // Asignamos el clip al VideoPlayer
        videoPlayer.clip = miVideo;

        // Reproducimos el video
        videoPlayer.Play();

        // Suscribimos al evento que se dispara cuando termina
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Cambiamos de escena al terminar el video
        SceneManager.LoadScene(nombreEscena);
    }
}

