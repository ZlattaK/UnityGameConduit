using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SphereReveal : MonoBehaviour
{
    [Header("Провода")]
    public Transform topWire;
    public Transform bottomWire;

    [Header("Финальные позиции проводов")]
    public Vector3 topWireTarget;
    public Vector3 bottomWireTarget;

    [Header("Объект")]
    public Transform targetObject;

    [Header("Финальная позиция объекта")]
    public Vector3 targetPosition;

    [Header("Видео")]
    public UnityEngine.Video.VideoPlayer videoPlayer;

    [Header("Скорость")]
    public float wireMoveDuration = 0.5f;
    public float moveDuration = 0.4f;

    [Header("Исчезновение проводов")]
    public float wireFadeDuration = 0.3f;

    private bool wiresOpened = false;
    private bool revealStarted = false;

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!wiresOpened)
            {
                wiresOpened = true;

                StartCoroutine(ShowWiresRoutine());
            }
        }
    }

    IEnumerator ShowWiresRoutine()
    {
        Vector3 topStart = topWire.position;
        Vector3 bottomStart = bottomWire.position;

        float t = 0f;

        while (t < wireMoveDuration)
        {
            t += Time.deltaTime;

            float k = t / wireMoveDuration;

            topWire.position =
                Vector3.Lerp(topStart, topWireTarget, k);

            bottomWire.position =
                Vector3.Lerp(bottomStart, bottomWireTarget, k);

            yield return null;
        }

        topWire.position = topWireTarget;
        bottomWire.position = bottomWireTarget;
    }

    public void StartReveal()
    {
        if (!revealStarted)
        {
            revealStarted = true;

            StartCoroutine(RevealSequence());
        }
    }

    IEnumerator MoveUpRoutine()
    {
        float t = 0f;

        Vector3 from = targetObject.position;
        Vector3 to = targetPosition;

        while (t < moveDuration)
        {
            t += Time.deltaTime;

            targetObject.position =
                Vector3.Lerp(from, to, t / moveDuration);

            yield return null;
        }

        targetObject.position = to;

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(FadeManager.Instance.FadeOut());

        if (videoPlayer != null)
        {
            videoPlayer.gameObject.SetActive(true);
            videoPlayer.Play();
        }

        yield return StartCoroutine(FadeManager.Instance.FadeIn());
    }

    IEnumerator RevealSequence()
    {
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(FadeWires());

        yield return StartCoroutine(MoveUpRoutine());
    }

    IEnumerator FadeWires()
    {
        SpriteRenderer topSR =
            topWire.GetComponent<SpriteRenderer>();

        SpriteRenderer bottomSR =
            bottomWire.GetComponent<SpriteRenderer>();

        float t = 0f;

        Color topColor = topSR.color;
        Color bottomColor = bottomSR.color;

        while (t < wireFadeDuration)
        {
            t += Time.deltaTime;

            float k = t / wireFadeDuration;

            topColor.a = Mathf.Lerp(1f, 0f, k);
            bottomColor.a = Mathf.Lerp(1f, 0f, k);

            topSR.color = topColor;
            bottomSR.color = bottomColor;

            yield return null;
        }

        topColor.a = 0f;
        bottomColor.a = 0f;

        topSR.color = topColor;
        bottomSR.color = bottomColor;

        topWire.gameObject.SetActive(false);
        bottomWire.gameObject.SetActive(false);
    }

    void OnVideoFinished(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(0);
    }
}