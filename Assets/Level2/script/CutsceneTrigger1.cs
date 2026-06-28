using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class CutsceneTrigger1 : MonoBehaviour
{
    public GameObject videoObject;
    public Vector3 cameraPosition = new Vector3(55f, 0.2264867f, -10f);

    [Header("Лис")]
    public GameObject fox;
    public float fadeDuration = 1f;

    [Header("Диалог")]
    public DialogController dialogController;

    [Header("Второй лис")]
    public GameObject foxSitting;

    [Header("Музыка после катсцены")]
    public AudioClip nextLocationMusic;

    private SpriteRenderer foxSittingRenderer;

    private string[] foxDialog = new string[]
    {
        "Добро пожаловать на край. Дальше этих мест уже никто ничего по имени не назовет. " +
        "Пограничье Нулевого уровня — здесь разбивали лагеря такие же учёные, как ты."
    };

    private VideoPlayer vp;
    private SpriteRenderer foxRenderer;

    void Start()
    {
        if (videoObject != null)
        {
            vp = videoObject.GetComponent<VideoPlayer>();
            videoObject.SetActive(false);

            if (vp != null)
                vp.loopPointReached += OnVideoFinished;
        }

        if (fox != null)
        {
            foxRenderer = fox.GetComponent<SpriteRenderer>();
            fox.SetActive(false);
        }

        if (foxSitting != null)
        {
            foxSittingRenderer = foxSitting.GetComponent<SpriteRenderer>();
            foxSitting.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if (videoObject != null)
        {
            videoObject.SetActive(true);

            if (vp != null)
                vp.Play();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        Camera.main.transform.position = cameraPosition;
        videoObject.SetActive(false);

        MusicManager music = FindObjectOfType<MusicManager>();
        if (music != null && nextLocationMusic != null)
        {
            music.PlayMusic(nextLocationMusic);
        }

        StartCoroutine(ShowFoxAndStartDialog());
    }

    IEnumerator ShowFoxAndStartDialog()
    {
        yield return new WaitForSeconds(0.3f);

        if (fox != null && foxRenderer != null)
        {
            fox.SetActive(true);

            Color color = foxRenderer.color;
            color.a = 0f;
            foxRenderer.color = color;

            float timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float t = Mathf.Clamp01(timer / fadeDuration);

                color.a = Mathf.Lerp(0f, 1f, t);
                foxRenderer.color = color;

                yield return null;
            }
        }

        if (dialogController != null)
            dialogController.StartDialog(foxDialog);

        while (dialogController != null && dialogController.IsRunning())
            yield return null;


        if (fox != null && foxRenderer != null)
        {
            Color color = foxRenderer.color;

            float timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float t = Mathf.Clamp01(timer / fadeDuration);

                color.a = Mathf.Lerp(1f, 0f, t);
                foxRenderer.color = color;

                yield return null;
            }

            fox.SetActive(false);
        }

        yield return new WaitForSeconds(0.2f);

        if (foxSitting != null && foxSittingRenderer != null)
        {
            foxSitting.SetActive(true);

            Color color = foxSittingRenderer.color;
            color.a = 0f;
            foxSittingRenderer.color = color;

            float timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float t = Mathf.Clamp01(timer / fadeDuration);

                color.a = Mathf.Lerp(0f, 1f, t);
                foxSittingRenderer.color = color;

                yield return null;
            }
        }
    }
}