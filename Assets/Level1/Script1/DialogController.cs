using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogController : MonoBehaviour
{
    [Header("Optional - you can assign manually or let script find them")]
    public GameObject dialogPanel;
    public TMP_Text dialogText;
    public Button nextButton;
    public Button closeButton;

    [Header("Typewriter settings")]
    public float charDelay = 0.04f;
    public float lineStayTime = 0.5f;

    private string[] lines;
    private int index;
    private Coroutine typingCoroutine;
    private bool isRunning = false;

    // диалог лиса с енотом
    [Header("Characters (optional)")]
    public SpriteRenderer foxRenderer;
    public SpriteRenderer raccoonRenderer;

    [Header("Dimming settings")]
    public float dimAlpha = 0.5f;
    public float normalAlpha = 1f;

    void Awake()
    {
        if (dialogPanel == null)
            dialogPanel = this.gameObject;

        if (dialogText == null)
            dialogText = dialogPanel.GetComponentInChildren<TMP_Text>();

        if (nextButton == null)
            nextButton = FindButtonByName("NextButton");
        if (closeButton == null)
            closeButton = FindButtonByName("CloseButton");

        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextButtonClicked);
        if (closeButton != null)
            closeButton.onClick.AddListener(ForceClose);

        if (dialogPanel != null)
            dialogPanel.SetActive(false);
    }

    private Button FindButtonByName(string name)
    {
        if (dialogPanel == null) return null;

        Transform t = dialogPanel.transform.Find(name);
        if (t != null)
            return t.GetComponent<Button>();

        Button b = dialogPanel.GetComponentInChildren<Button>(true);
        if (b != null && b.name == name) return b;

        return dialogPanel.GetComponentInChildren<Button>(true);
    }

    public void StartDialog(string[] linesToShow)
    {
        if (linesToShow == null || linesToShow.Length == 0)
            return;

        lines = linesToShow;
        index = 0;
        isRunning = true;

        if (dialogPanel != null)
            dialogPanel.SetActive(true);

        StartNextLine();
    }

    private void StartNextLine()
    {
        if (lines == null || index < 0 || index >= lines.Length)
        {
            EndDialog();
            return;
        }

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeAndHold(lines[index]));
    }

    private void ApplySpeaker(string speaker)
    {
        if (foxRenderer == null || raccoonRenderer == null) return;

        if (speaker == "В") // Лис говорит
        {
            foxRenderer.color = Color.white; // нормальный цвет
            raccoonRenderer.color = new Color(0.4f, 0.4f, 0.4f, 1f); // слегка затемнён
        }
        else if (speaker == "Е") // Енот говорит
        {
            foxRenderer.color = new Color(0.4f, 0.4f, 0.4f, 1f);
            raccoonRenderer.color = Color.white;
        }
    }

    IEnumerator TypeAndHold(string fullLine)
    {
        if (dialogText == null)
            yield break;

        string speaker = "";
        string textToShow = fullLine;

        int separatorIndex = fullLine.IndexOf('|');
        if (separatorIndex > 0)
        {
            speaker = fullLine.Substring(0, separatorIndex);
            textToShow = fullLine.Substring(separatorIndex + 1);

            ApplySpeaker(speaker);
        }

        dialogText.text = "";

        foreach (char c in textToShow)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(charDelay);
        }

        yield return new WaitForSeconds(lineStayTime);

        dialogText.text = "";

        index++;
        if (index < lines.Length)
        {
            yield return new WaitForSeconds(0.1f);
            StartNextLine();
        }
        else
        {
            EndDialog();
        }
    }

    public void OnNextButtonClicked()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            if (dialogText != null && lines != null && index < lines.Length)
                dialogText.text = lines[index];
            typingCoroutine = null;
            return;
        }

        index++;
        if (index < lines.Length)
            StartNextLine();
        else
            EndDialog();
    }

    public void ForceClose()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        EndDialog();
    }

    private void EndDialog()
    {
        isRunning = false;

        if (dialogPanel != null)
            dialogPanel.SetActive(false);

        if (dialogText != null)
            dialogText.text = "";

        lines = null;
    }

    public bool IsRunning()
    {
        return isRunning;
    }
}