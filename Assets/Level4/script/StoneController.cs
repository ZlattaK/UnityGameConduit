using UnityEngine;
using System.Collections;

public class StoneController : MonoBehaviour
{
    [Header("Диалог при ПКМ")]
    public DialogController dialogController;
    [TextArea] public string[] defaultLines;

    [Header("Диалог при навигаторе")]
    [TextArea] public string[] navigatorLines;

    private bool navigatorUsed = false;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            if (!navigatorUsed)
            {
                if (dialogController != null && defaultLines.Length > 0)
                    dialogController.StartDialog(defaultLines);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (navigatorUsed) return;

        if (other.CompareTag("Navigator"))
        {
            navigatorUsed = true;

            other.gameObject.SetActive(false);

            StartCoroutine(StartDialog());

            Debug.Log("Навигатор использован на камне");
        }
    }

    IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(0.2f);

        if (dialogController != null && navigatorLines.Length > 0)
            dialogController.StartDialog(navigatorLines);
    }
}