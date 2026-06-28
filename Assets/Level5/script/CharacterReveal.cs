using UnityEngine;
using System.Collections;

public class CharacterReveal : MonoBehaviour
{
    [Header("Спрайты")]
    public SpriteRenderer backSprite;
    public SpriteRenderer frontSprite;

    [Header("Скорость")]
    public float fadeDuration = 0.2f;

    [Header("Диалог")]
    public DialogController dialogController;
    [TextArea] public string[] dialogLines;

    private bool isTriggered;

    void Start()
    {
        frontSprite.gameObject.SetActive(true);

        SetAlpha(backSprite, 1f);
        SetAlpha(frontSprite, 0f);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(RevealRoutine());
        }
    }

    IEnumerator RevealRoutine()
    {
        yield return Fade(backSprite, 1f, 0f);

        backSprite.enabled = false;
        SetAlpha(backSprite, 0f);

        yield return Fade(frontSprite, 0f, 1f);

        SetAlpha(frontSprite, 1f);

        if (dialogController != null && dialogLines.Length > 0)
            dialogController.StartDialog(dialogLines);
    }

    IEnumerator Fade(SpriteRenderer sr, float from, float to)
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(from, to, t / fadeDuration);
            SetAlpha(sr, a);
            yield return null;
        }

        SetAlpha(sr, to);
    }

    void SetAlpha(SpriteRenderer sr, float a)
    {
        Color c = sr.color;
        c.a = a;
        sr.color = c;
    }
}