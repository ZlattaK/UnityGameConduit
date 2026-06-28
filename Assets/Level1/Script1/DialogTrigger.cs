using UnityEngine;
using System;

public class DialogTrigger : MonoBehaviour
{
    public DialogController dialogController;
    [TextArea] public string[] lines;

    public event Action OnDialogStarted;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            if (dialogController != null && lines != null && lines.Length > 0)
            {
                dialogController.StartDialog(lines);

                OnDialogStarted?.Invoke();
            }
        }
    }
}