using UnityEngine;

public class FoxInteraction : MonoBehaviour
{
    [Header("Диалог")]
    public DialogController dialogController;

    [TextArea]
    public string[] foxLines = new string[]
    {
        "Я тебе не гид, иди займись чем-нибудь полезным."
    };

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogController != null)
            {
                dialogController.StartDialog(foxLines);
            }
        }
    }
}