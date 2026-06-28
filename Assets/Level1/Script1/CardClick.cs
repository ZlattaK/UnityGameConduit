using UnityEngine;
using System.Collections;

public class CardClick : MonoBehaviour
{
    public DialogController dialogController;
    public string[] cardDialogLines;
    public float dialogDelay = 0.2f;
    public GameObject nextSceneButton;

    private bool clicked = false;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("CARD CLICKED");

            if (clicked) return;
            clicked = true;

            if (dialogController != null && cardDialogLines.Length > 0)
            {
                StartCoroutine(ShowDialogDelayed());
                Debug.Log("Button is: " + nextSceneButton);
            }
        }
    }

    private IEnumerator ShowDialogDelayed()
    {
        yield return new WaitForSeconds(dialogDelay);

        dialogController.StartDialog(cardDialogLines);

        while (dialogController != null && dialogController.IsRunning())
        {
            yield return null;
        }

        if (nextSceneButton != null)
            nextSceneButton.SetActive(true);
    }
}