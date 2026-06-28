using UnityEngine;

public class NPCDialogTrigger : MonoBehaviour
{
    public DialogController dialogController;
    [TextArea] public string[] lines;
    public string npcName;

    [TextArea] public string[] finalDialog;

    private static System.Collections.Generic.Dictionary<string, bool> npcCompleted =
        new System.Collections.Generic.Dictionary<string, bool>()
    {
        {"Boar", false},
        {"Rat", false},
        {"Fox", false}
    };

    private bool isTalking = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D col = GetComponent<Collider2D>();
            if (col != null && col.OverlapPoint(mousePos))
            {
                if (!isTalking)
                {
                    StartCoroutine(StartDialogAndCheck());
                }
            }
        }
    }

    private System.Collections.IEnumerator StartDialogAndCheck()
    {
        isTalking = true;

        dialogController.StartDialog(lines);

        while (dialogController.IsRunning())
            yield return null;

        if (npcCompleted.ContainsKey(npcName))
            npcCompleted[npcName] = true;

        bool allDone = true;
        foreach (var val in npcCompleted.Values)
        {
            if (!val)
            {
                allDone = false;
                break;
            }
        }

        if (allDone && finalDialog != null && finalDialog.Length > 0)
        {
            yield return new WaitForSeconds(1f);
            dialogController.StartDialog(finalDialog);

            while (dialogController.IsRunning())
                yield return null;
        }

        isTalking = false;
    }
}