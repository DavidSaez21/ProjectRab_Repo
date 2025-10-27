using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;  // Solo para poder usar SceneAsset en el Inspector
#endif

public class IntroPlayer_Clip_SceneAsset : MonoBehaviour
{
    [Header("Video de introducción")]
    public VideoClip introClip;

    [Header("Escena siguiente")]
#if UNITY_EDITOR
    public SceneAsset nextSceneAsset;  // Asignable desde el inspector
#endif
    [HideInInspector] public string nextSceneName;

    private VideoPlayer videoPlayer;

    void Awake()
    {
#if UNITY_EDITOR
        // Guardar el nombre de la escena seleccionada (solo en el editor)
        if (nextSceneAsset != null)
            nextSceneName = nextSceneAsset.name;
#endif
    }

    void Start()
    {
        // Crear el reproductor
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = introClip;
        videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        videoPlayer.targetCamera = Camera.main;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;

        videoPlayer.loopPointReached += OnVideoEnd;

        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += (vp) => vp.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log("Cargando escena: " + nextSceneName);
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("No se ha asignado la siguiente escena.");
        }
    }
}