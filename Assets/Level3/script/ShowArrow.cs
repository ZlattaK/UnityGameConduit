using UnityEngine;
using System.Collections;

public class ShowArrow : MonoBehaviour
{
    public DialogTrigger dialogTrigger;
    public GameObject arrowObject;

    public float delay = 5f;

    void Start()
    {
        if (arrowObject != null)
            arrowObject.SetActive(false);

        if (dialogTrigger != null)
            dialogTrigger.OnDialogStarted += StartArrowTimer;
    }

    void StartArrowTimer()
    {
        StartCoroutine(ShowAfterDelay());
    }

    IEnumerator ShowAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        if (arrowObject != null)
            arrowObject.SetActive(true);
    }

    void OnDestroy()
    {
        if (dialogTrigger != null)
            dialogTrigger.OnDialogStarted -= StartArrowTimer;
    }
}