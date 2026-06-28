using UnityEngine;
using System.Collections;

public class CombineItems : MonoBehaviour
{
    public Transform itemA;
    public Transform itemB;

    [Header("Result (Triangle)")]
    public GameObject triangle; 
    public float combineDistance = 0.7f;

    [Header("Газета")]
    public GameObject foldedNewspaperPrefab;
    public GameObject unfoldedNewspaperPrefab;
    public GameObject businessCardPrefab;
    public Transform newspaperParent;
    public Vector3 newspaperPosition = new Vector3(-9.84f, -5.33f, 0f);

    [Header("Диалог")]
    public DialogController dialogController;
    [TextArea] public string[] dialogLines;
    public float dialogDelay = 1.5f;

    private bool combined = false;
    private bool dialogShown = false;
    private bool newspaperSpawned = false;

    [Header("Кнопка перехода")]
    public GameObject nextSceneButton;

    public static bool hasCrafted = false;

    void Start()
    {
        if (triangle != null)
            triangle.SetActive(false);
    }

    void Update()
    {
        if (combined) return;

        if (itemA == null || itemB == null) return;
        if (!itemA.gameObject.activeSelf || !itemB.gameObject.activeSelf) return;

        float dist = Vector2.Distance(itemA.position, itemB.position);
        if (dist <= combineDistance)
        {
            Combine();
        }
    }

    void Combine()
    {
        combined = true;
        hasCrafted = true;

        itemA.gameObject.SetActive(false);
        itemB.gameObject.SetActive(false);

        if (triangle != null)
        {
            triangle.SetActive(true);

            Collider2D col = triangle.GetComponent<Collider2D>();
            if (col != null) col.enabled = true;

            DraggableSprite ds = triangle.GetComponent<DraggableSprite>();
            if (ds != null)
            {
                ds.slots = FindObjectsOfType<InventorySlot>(true);
                Debug.Log("[Combine] Triangle слоты: " + ds.slots.Length);
            }
        }

        if (!dialogShown && dialogController != null && dialogLines.Length > 0)
        {
            StartCoroutine(ShowDialogDelayed());
            dialogShown = true;
        }

        if (!newspaperSpawned && foldedNewspaperPrefab != null)
        {
            SpawnNewspaper();
            newspaperSpawned = true;
        }
    }

    private IEnumerator ShowDialogDelayed()
    {
        yield return new WaitForSeconds(dialogDelay);
        dialogController.StartDialog(dialogLines);
    }

    private void SpawnNewspaper()
    {
        GameObject newspaper = Instantiate(foldedNewspaperPrefab, newspaperPosition, Quaternion.identity);

        if (newspaperParent != null)
            newspaper.transform.SetParent(newspaperParent);

        NewspaperClick nc = newspaper.AddComponent<NewspaperClick>();
        nc.unfoldedPrefab = unfoldedNewspaperPrefab;
        nc.businessCardPrefab = businessCardPrefab;
        nc.nextSceneButton = nextSceneButton;
        nc.dialogController = dialogController;
    }
}