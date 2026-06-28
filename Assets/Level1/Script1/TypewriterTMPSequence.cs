using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterTMPSequence : MonoBehaviour
{
    public TMP_Text textComponent;

    [TextArea]
    public string[] lines;

    public float startDelay = 1f;         
    public float charDelay = 0.05f;       
    public float pauseAfterLine = 1.5f;   
    public float pauseBeforeNext = 0.5f;  

    public bool skipOnClick = true;

    void Start()
    {
        if (textComponent == null)
            textComponent = GetComponent<TMP_Text>();

        StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        yield return new WaitForSeconds(startDelay);

        foreach (string line in lines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(pauseAfterLine);
            textComponent.text = "";
            yield return new WaitForSeconds(pauseBeforeNext);
        }

        gameObject.SetActive(false);
    }

    IEnumerator TypeLine(string line)
    {
        textComponent.text = "";

        for (int i = 0; i < line.Length; i++)
        {
            textComponent.text += line[i];
            yield return new WaitForSeconds(charDelay);

            if (skipOnClick && Input.GetMouseButtonDown(0))
            {
                textComponent.text = line;
                break;
            }
        }
    }
}
