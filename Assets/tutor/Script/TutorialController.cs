using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour
{
    [Header("Спрайты")]
    public SpriteRenderer[] notes;

    [Header("Иконки мыши")]
    public SpriteRenderer pkmIcon;
    public SpriteRenderer lkmIcon;
    public SpriteRenderer lkm2Icon;

    [Header("Настройки")]
    public float fadeDuration = 0.2f;

    [Header("Время жизни записок")]
    public float note1Life = 6f;
    public float note2Delay = 4f;

    public float note2Life = 6f;
    public float note3Delay = 4f;

    public float note3Life = 6f;
    public float note4Delay = 3f;

    public float note4Life = 6f;
    public float note5Delay = 2f;

    public float note5Life = 2f;

    public void StartTutorial()
    {
        StartCoroutine(TutorialRoutine());
    }

    IEnumerator TutorialRoutine()
    {
        StartCoroutine(NoteWithIcon(notes[0], pkmIcon, note1Life));

        yield return new WaitForSeconds(note2Delay);

        StartCoroutine(NoteWithIcon(notes[1], lkmIcon, note2Life));

        yield return new WaitForSeconds(note3Delay);

        StartCoroutine(NoteWithIcon(notes[2], lkm2Icon, note3Life));

        yield return new WaitForSeconds(note4Delay);

        StartCoroutine(NoteCycle(notes[3], note4Life));

        yield return new WaitForSeconds(note5Delay);

        StartCoroutine(NoteCycle(notes[4], note5Life));
    }

    IEnumerator NoteCycle(SpriteRenderer sr, float lifeTime)
    {
        yield return StartCoroutine(FadeIn(sr));

        yield return new WaitForSeconds(lifeTime);

        yield return StartCoroutine(FadeOut(sr));

        sr.gameObject.SetActive(false);
    }

    IEnumerator NoteWithIcon(SpriteRenderer note, SpriteRenderer icon, float lifeTime)
    {
        note.gameObject.SetActive(true);
        icon.gameObject.SetActive(true);

        yield return StartCoroutine(FadeIn(note));
        yield return StartCoroutine(FadeIn(icon));

        yield return new WaitForSeconds(lifeTime);

        yield return StartCoroutine(FadeOut(note));
        yield return StartCoroutine(FadeOut(icon));

        note.gameObject.SetActive(false);
        icon.gameObject.SetActive(false);
    }

    IEnumerator FadeIn(SpriteRenderer sr)
    {
        float t = 0f;
        Color c = sr.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            sr.color = c;
            yield return null;
        }

        c.a = 1f;
        sr.color = c;
    }

    IEnumerator FadeOut(SpriteRenderer sr)
    {
        float t = 0f;
        Color c = sr.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            sr.color = c;
            yield return null;
        }

        c.a = 0f;
        sr.color = c;
    }
}