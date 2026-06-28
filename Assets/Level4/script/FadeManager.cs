using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    [Header("UI")]
    public Canvas canvas;
    public Image fadeImage;

    [Header("Settings")]
    public float fadeDuration = 0.5f;

    void Awake()
    {
        Instance = this;

        if (canvas != null)
            canvas.gameObject.SetActive(false);
    }

    public IEnumerator FadeOut()
    {
        yield return StartCoroutine(Fade(1f));
    }

    public IEnumerator FadeIn()
    {
        yield return StartCoroutine(Fade(0f));

        if (canvas != null)
            canvas.gameObject.SetActive(false);
    }

    IEnumerator Fade(float targetAlpha)
    {
        if (canvas != null)
            canvas.gameObject.SetActive(true);

        float startAlpha = fadeImage.color.a;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            float a = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);

            fadeImage.color = new Color(0, 0, 0, a);

            yield return null;
        }
    }
}