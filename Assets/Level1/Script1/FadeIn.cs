using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.5f;

    [Header("Tutorial")]
    public TutorialController tutorialController;
    public float tutorialStartPoint = 1f; 

    private bool tutorialStarted = false;

    void Start()
    {
        if (fadeImage == null)
            fadeImage = GetComponent<Image>();

        StartCoroutine(FadeFromBlack());
    }

    IEnumerator FadeFromBlack()
    {
        Color c = fadeImage.color;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            float progress = elapsed / fadeDuration;

            c.a = Mathf.Lerp(1f, 0f, progress);
            fadeImage.color = c;

         
            if (!tutorialStarted && progress >= tutorialStartPoint)
            {
                tutorialStarted = true;

                if (tutorialController != null)
                {
                    tutorialController.StartTutorial();
                }
            }

            yield return null;
        }

        c.a = 0f;
        fadeImage.color = c;
        fadeImage.gameObject.SetActive(false);
    }
}