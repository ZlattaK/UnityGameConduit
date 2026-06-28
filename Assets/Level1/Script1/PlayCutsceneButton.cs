using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class PlayCutsceneButton : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public Vector3 cameraPosition = new Vector3(30.54f, -0.1f, -10f);

    [Header("Музыка после катсцены")]
    public AudioClip nextLocationMusic;

    [Header("Инвентарь")]
    public InventoryManager inventoryManager;

    [Header("Диалог при нехватке предметов")]
    public DialogController dialogController;

    [TextArea]
    public string[] notEnoughItemsDialog;

    private void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    public void PlayCutscene()
    {
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager не назначен!");
            return;
        }

        if (inventoryManager == null || !inventoryManager.HasAtLeastItems(2))
        {
            if (dialogController != null && notEnoughItemsDialog.Length > 0)
            {
                dialogController.StartDialog(notEnoughItemsDialog);
            }

            return;
        }

        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        StartCoroutine(MoveCameraNextFrame());
    }

    IEnumerator MoveCameraNextFrame()
    {
        yield return null;

        Camera.main.transform.position = cameraPosition;

        MusicManager music = FindObjectOfType<MusicManager>();
        if (music != null && nextLocationMusic != null)
        {
            music.PlayMusic(nextLocationMusic);
        }

        ItemLocationHandler[] items = FindObjectsOfType<ItemLocationHandler>();

        foreach (var item in items)
        {
            item.currentLocation = ItemLocationHandler.Location.Bar;
        }

        videoPlayer.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}