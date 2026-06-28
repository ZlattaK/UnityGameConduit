using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxController : MonoBehaviour
{
    [Header("Ящик")]
    public GameObject boxOpen;

    [Header("Пазлы")]
    public GameObject puzzle;
    public GameObject formPuzzle;

    [Header("Диалог")]
    public DialogController dialogController;

    [Header("Диалог линии")]
    [TextArea]
    public string[] dialogLines;

    [Header("Задержка")]
    public float dialogDelay = 0.3f;

    private bool boxIsOpen = false;
    private bool puzzleCompleted = false;

    private List<PuzzlePiece> pieces = new List<PuzzlePiece>();
    private int placedCount = 0;

    void Start()
    {
        if (boxOpen != null) boxOpen.SetActive(false);
        if (puzzle != null) puzzle.SetActive(false);
        if (formPuzzle != null) formPuzzle.SetActive(false);

        Debug.Log("START: BoxController init");

        pieces.AddRange(puzzle.GetComponentsInChildren<PuzzlePiece>());

        Debug.Log("TOTAL PIECES FOUND: " + pieces.Count);

        foreach (var p in pieces)
        {
            Debug.Log("PIECE FOUND: " + p.name + " active=" + p.gameObject.activeInHierarchy);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null)
            {
                Debug.Log("RIGHT CLICK HIT: " + hit.name);
            }

            if (hit != null && hit.gameObject == gameObject)
            {
                if (!boxIsOpen)
                    OpenBox();
            }
            else
            {
                if (boxIsOpen)
                    CloseBox();
            }
        }
    }

    void OpenBox()
    {
        Debug.Log("OPEN BOX");

        if (boxOpen != null) boxOpen.SetActive(true);
        if (puzzle != null) puzzle.SetActive(true);
        if (formPuzzle != null) formPuzzle.SetActive(true);

        boxIsOpen = true;
    }

    void CloseBox()
    {
        Debug.Log("CLOSE BOX");

        if (boxOpen != null) boxOpen.SetActive(false);
        if (puzzle != null) puzzle.SetActive(false);
        if (formPuzzle != null) formPuzzle.SetActive(false);

        boxIsOpen = false;
    }

    public void RegisterPiecePlaced()
    {
        if (puzzleCompleted) return;

        placedCount++;

        Debug.Log("PLACED COUNT: " + placedCount + " / " + pieces.Count);

        if (placedCount >= pieces.Count)
        {
            Debug.Log("PUZZLE COMPLETE DETECTED");

            puzzleCompleted = true;
            StartCoroutine(StartDialogWithDelay());
        }
    }

    IEnumerator StartDialogWithDelay()
    {
        Debug.Log("WAITING BEFORE DIALOG: " + dialogDelay);

        yield return new WaitForSeconds(dialogDelay);

        Debug.Log("START DIALOG");

        if (dialogController != null)
        {
            dialogController.StartDialog(dialogLines);
        }
        else
        {
            Debug.Log("DIALOG CONTROLLER IS NULL");
        }
    }
}