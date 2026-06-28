using UnityEngine;
using System.Collections;

public class SwapCharacter : MonoBehaviour
{
    [Header("Первый персонаж")]
    public GameObject oldCharacter;
    public GameObject newCharacter;
    public float swapDelay = 0.5f;
    public float fadeDuration = 0.5f;

    [Header("Второй персонаж")]
    public GameObject secondOldCharacter;
    public GameObject secondNewCharacter;
    public float secondSwapDelay = 2f;

    [Header("Отслеживаемый предмет")]
    public GameObject itemToWatch;

    [Header("Analyzator")]
    public GameObject analyzator;

    [Header("Dialog")]
    public DialogController dialogController;

    [Header("Камень")]
    public GameObject stoneBar;
    public float stoneFadeDuration = 1f;

    [Header("Спрайт-кнопка катсцены")]
    public GameObject squareButton; 

    private string[] dialogLines = new string[]
    {
        "В|Брось, бумаги не спасут.",
        "Е|Извините?",
        "В|Этот камень не меняет фазу. Он меняет людей.",
        "Е|Вы что эксперт?",
        "В|А ты из института? Все вы умные пока дело до практики не доходит."
    };

    private string[] dialogAfterStone = new string[]
    {
        "В|Узнаешь?",
        "Е|Не может быть, я не думал, что у кого-то будет такой же экземпляр, нужно проверить на портативном анализаторе не пустышка ли это."
    };

    private string[] finalDialog = new string[]
    {
        "Е|Не верю… показатели не стабильны",
        "В|Ого, да ты профессор, а что значит не стабильны.",
        "Е|Камень активен даже здесь, если анализатор не врет, то он должен вести себя иначе в своей природной среде.",
        "В|Эта игрушка правда работает? Камень-то с нулевого уровня.",
        "Е|Оттуда и вправду кто-то возвращался? Прошу, отвезите меня туда, это дело всей моей жизни!",
        "В|Отдай мне свою игрушку, а я отвезу тебя хоть на край земли."
    };

    private string[] lastPhrase = new string[]
    {
        "В|Завтра, старый мост, на рассвете. Ждать не буду."
    };

    private ItemLocationHandler locationHandler;
    private DraggableSprite draggable;

    private DraggableSprite analyzatorDraggable;
    private ItemLocationHandler analyzatorLocation;

    private bool swapped = false;
    private bool ignoreFirstFrame = true;
    private bool finalDialogStarted = false;

    private bool waitingForGive = false;
    private bool analyzerGiven = false;

    private SpriteRenderer oldRenderer;
    private SpriteRenderer newRenderer;

    private SpriteRenderer secondOldRenderer;
    private SpriteRenderer secondNewRenderer;

    private Collider2D wolfCollider;

    void Start()
    {
        if (squareButton != null)
            squareButton.SetActive(false);

        if (itemToWatch != null)
        {
            locationHandler = itemToWatch.GetComponent<ItemLocationHandler>();
            draggable = itemToWatch.GetComponent<DraggableSprite>();
        }

        if (analyzator != null)
        {
            analyzatorLocation = analyzator.GetComponent<ItemLocationHandler>();
            analyzatorDraggable = analyzator.GetComponent<DraggableSprite>();
        }

        if (oldCharacter != null)
            oldRenderer = oldCharacter.GetComponent<SpriteRenderer>();

        if (newCharacter != null)
        {
            newRenderer = newCharacter.GetComponent<SpriteRenderer>();
            newCharacter.SetActive(false);

            wolfCollider = newCharacter.GetComponent<Collider2D>();
            if (wolfCollider != null)
                wolfCollider.enabled = false;
        }

        if (secondOldCharacter != null)
            secondOldRenderer = secondOldCharacter.GetComponent<SpriteRenderer>();

        if (secondNewCharacter != null)
        {
            secondNewRenderer = secondNewCharacter.GetComponent<SpriteRenderer>();
            secondNewCharacter.SetActive(false);
        }

        if (draggable != null)
            draggable.wasJustTakenOut = false;

        if (stoneBar != null)
            stoneBar.SetActive(false);
    }

    void Update()
    {
        if (!swapped && draggable != null && locationHandler != null)
        {
            if (ignoreFirstFrame)
                ignoreFirstFrame = false;
            else if (draggable.currentSlot == null && draggable.wasJustTakenOut &&
                     locationHandler.currentLocation == ItemLocationHandler.Location.Bar)
            {
                draggable.wasJustTakenOut = false;
                swapped = true;
                StartCoroutine(SwapAfterDelay());
            }
        }

        if (!finalDialogStarted && analyzatorDraggable != null && analyzatorLocation != null)
        {
            bool isTakenOut = analyzatorDraggable.currentSlot == null && analyzatorDraggable.wasJustTakenOut;
            bool inBar = analyzatorLocation.currentLocation == ItemLocationHandler.Location.Bar;

            if (isTakenOut && inBar)
            {
                analyzatorDraggable.wasJustTakenOut = false;
                finalDialogStarted = true;
                StartCoroutine(ContinueFinalDialog());
            }
        }

        if (waitingForGive && !analyzerGiven && analyzatorDraggable != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D hit = Physics2D.OverlapPoint(mousePos);

                if (hit != null && hit.gameObject == newCharacter)
                {
                    analyzerGiven = true;
                    waitingForGive = false;

                    analyzator.SetActive(false);

                    if (wolfCollider != null)
                        wolfCollider.enabled = false;

                    dialogController.StartDialog(lastPhrase);
                    StartCoroutine(ShowSquareAfterDialog());
                }
            }
        }
    }

    private IEnumerator SwapAfterDelay()
    {
        yield return new WaitForSeconds(swapDelay);

        newCharacter.SetActive(true);

        Color newColor = newRenderer.color;
        newColor.a = 0f;
        newRenderer.color = newColor;

        Color oldColor = oldRenderer.color;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;

            oldColor.a = Mathf.Lerp(1f, 0f, t);
            newColor.a = Mathf.Lerp(0f, 1f, t);

            oldRenderer.color = oldColor;
            newRenderer.color = newColor;

            yield return null;
        }

        oldCharacter.SetActive(false);

        if (itemToWatch != null)
            itemToWatch.SetActive(false);

        dialogController.StartDialog(dialogLines);
        StartCoroutine(WaitForDialogEndThenShowStone());

        yield return new WaitForSeconds(secondSwapDelay);

        secondNewCharacter.SetActive(true);

        Color secondNewColor = secondNewRenderer.color;
        secondNewColor.a = 0f;
        secondNewRenderer.color = secondNewColor;

        Color secondOldColor = secondOldRenderer.color;

        float t2 = 0f;
        while (t2 < fadeDuration)
        {
            t2 += Time.deltaTime;
            float t = t2 / fadeDuration;

            secondOldColor.a = Mathf.Lerp(1f, 0f, t);
            secondNewColor.a = Mathf.Lerp(0f, 1f, t);

            secondOldRenderer.color = secondOldColor;
            secondNewRenderer.color = secondNewColor;

            yield return null;
        }

        secondOldCharacter.SetActive(false);
    }

    private IEnumerator WaitForDialogEndThenShowStone()
    {
        while (dialogController.IsRunning())
            yield return null;

        if (stoneBar != null)
        {
            stoneBar.SetActive(true);

            SpriteRenderer sr = stoneBar.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Color color = sr.color;
                color.a = 0f;
                sr.color = color;

                float timer = 0f;
                while (timer < stoneFadeDuration)
                {
                    timer += Time.deltaTime;
                    float t = Mathf.Clamp01(timer / stoneFadeDuration);

                    color.a = Mathf.Lerp(0f, 1f, t);
                    sr.color = color;

                    yield return null;
                }
            }
        }

        yield return new WaitForSeconds(0.5f);

        dialogController.StartDialog(dialogAfterStone);
    }

    private IEnumerator ContinueFinalDialog()
    {
        yield return new WaitForSeconds(0.5f);

        dialogController.StartDialog(finalDialog);

        while (dialogController.IsRunning())
            yield return null;

        if (wolfCollider != null)
            wolfCollider.enabled = true;

        waitingForGive = true;
    }

    private IEnumerator ShowSquareAfterDialog()
    {
        while (dialogController.IsRunning())
            yield return null;

        if (squareButton != null)
            squareButton.SetActive(true);
    }
}