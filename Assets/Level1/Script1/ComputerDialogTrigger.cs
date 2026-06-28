using UnityEngine;

public class ComputerDialogTrigger : MonoBehaviour
{
    [Header("Диалог")]
    public DialogController dialogController;  
    [TextArea] public string[] lines;          

    [Header("Компьютер")]
    public PasswordPanelController passwordController; 

    private void Awake()
    {
        if (passwordController != null)
        {
            passwordController.OnComputerClosed += TriggerDialog;
        }
    }

    private void OnDestroy()
    {
        if (passwordController != null)
        {
            passwordController.OnComputerClosed -= TriggerDialog;
        }
    }

    private void TriggerDialog()
    {
        if (dialogController != null && lines != null && lines.Length > 0)
        {
            dialogController.StartDialog(lines);
        }
    }
}
