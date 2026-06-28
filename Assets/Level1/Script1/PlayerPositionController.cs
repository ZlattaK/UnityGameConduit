using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class PlayerPositionController : MonoBehaviour
{
    [Header("Стартовые позиции")]
    public Vector3 startPosition = new Vector3(-6.98f, -1.51f, 0f); 
    public Vector3 afterCutscenePosition = new Vector3(16.28f, -5.48f, 0f); 

    [Header("Катсцена")]
    public VideoPlayer videoPlayer; 

    private void Start()
    {
        transform.position = startPosition;

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnCutsceneFinished;
        }
    }

    private void OnCutsceneFinished(VideoPlayer vp)
    {
        transform.position = afterCutscenePosition;
    }
}