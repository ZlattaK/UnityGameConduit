using UnityEngine;

public class NewspaperClick : MonoBehaviour
{
    [Header("Префабы")]
    public GameObject unfoldedPrefab;
    public GameObject businessCardPrefab;

    [Header("Настройки")]
    public float dialogDelay = 0.2f;

    [Header("Кнопка перехода")]
    public GameObject nextSceneButton;

    [Header("Позиция карточки")]
    public Transform cardSpawnOffset;

    [Header("Диалог")]
    public DialogController dialogController;

    private GameObject currentUnfolded;
    private GameObject currentCard;

    private bool isOpened = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D myCol = GetComponent<Collider2D>();

            if (myCol != null && myCol.OverlapPoint(mousePos))
            {
                OpenNewspaper();
                return;
            }

            if (isOpened && currentUnfolded != null)
            {
                Collider2D unfoldedCol =
                    currentUnfolded.GetComponent<Collider2D>();

                bool clickedOnNewspaper = false;

                if (unfoldedCol != null)
                {
                    clickedOnNewspaper =
                        unfoldedCol.OverlapPoint(mousePos);
                }

                bool clickedOnCard = false;

                if (currentCard != null)
                {
                    Collider2D cardCol =
                        currentCard.GetComponent<Collider2D>();

                    if (cardCol != null)
                    {
                        clickedOnCard =
                            cardCol.OverlapPoint(mousePos);
                    }
                }

                if (!clickedOnNewspaper && !clickedOnCard)
                {
                    CloseNewspaper();
                }
            }
        }
    }

    void OpenNewspaper()
    {
        if (isOpened) return;

        Vector3 center = Vector3.zero;

        if (unfoldedPrefab != null)
        {
            currentUnfolded =
                Instantiate(unfoldedPrefab, center, Quaternion.identity);
        }

        if (businessCardPrefab != null)
        {
            Vector3 cardPos;

            if (cardSpawnOffset != null)
                cardPos = cardSpawnOffset.position;
            else
                cardPos = center + new Vector3(1f, -1f, 0f);

            currentCard =
                Instantiate(businessCardPrefab, cardPos, Quaternion.identity);

            CardClick cc = currentCard.GetComponent<CardClick>();

            if (cc == null)
                cc = currentCard.AddComponent<CardClick>();

            cc.dialogController = dialogController;

            cc.cardDialogLines = new string[]
            {
                "Я слышал об этом месте, там много торговцев информацией. Терять мне все равно нечего…"
            };

            cc.dialogDelay = dialogDelay;
            cc.nextSceneButton = nextSceneButton;
        }

        isOpened = true;
    }

    void CloseNewspaper()
    {
        if (currentUnfolded != null)
            Destroy(currentUnfolded);

        if (currentCard != null)
            Destroy(currentCard);

        isOpened = false;
    }
}