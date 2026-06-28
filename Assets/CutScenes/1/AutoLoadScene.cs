using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class AutoMoveCameraAfterVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public Vector3 cameraPosition = new Vector3(27.42f, 0.3f, -10f);

    private void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        StartCoroutine(MoveCameraNextFrame());
    }

    IEnumerator MoveCameraNextFrame()
    {
        yield return null; // ждём 1 кадр
        Camera.main.transform.position = cameraPosition;
    }
}