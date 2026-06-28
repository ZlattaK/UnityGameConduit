using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class CutsceneLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextScene = "Level1";

    private void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextScene);
    }

    public void SkipCutscene()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }

        SceneManager.LoadScene(nextScene);
    }
}