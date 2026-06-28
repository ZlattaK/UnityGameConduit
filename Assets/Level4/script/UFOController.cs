using UnityEngine;

public class UFOController : MonoBehaviour
{
    [Header("Контроллер растений")]
    public PlantController plantController;

    [Header("Диалог (если заблокировано)")]
    public DialogController dialogController;

    [TextArea]
    public string[] blockedLines;

    [Header("Диалог после открытия")]
    public string[] openLines;

    [Header("Открытая тарелка")]
    public GameObject ufoOpen;

    private bool isOpen = false;

    void Start()
    {
        if (ufoOpen != null)
            ufoOpen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.gameObject == gameObject)
            {
                OnUFOClicked();
            }
            else
            {
                CloseUFO();
            }
        }
    }

    void OnUFOClicked()
    {
        if (plantController != null && plantController.IsBlocked())
        {
            if (dialogController != null && blockedLines != null && blockedLines.Length > 0)
                dialogController.StartDialog(blockedLines);

            return;
        }

        OpenUFO();
    }

    void OpenUFO()
    {
        if (isOpen) return;

        isOpen = true;

        if (ufoOpen != null)
            ufoOpen.SetActive(true);

        if (dialogController != null && openLines != null && openLines.Length > 0)
            dialogController.StartDialog(openLines);
    }

    void CloseUFO()
    {
        if (!isOpen) return;

        isOpen = false;

        if (ufoOpen != null)
            ufoOpen.SetActive(false);
    }
}