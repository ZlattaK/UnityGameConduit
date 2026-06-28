using UnityEngine;

public class TruckController : MonoBehaviour
{
    [Header("Грузовик")]
    public GameObject truckOpen;

    [Header("Блокнот")]
    public GameObject notebookClosed;
    public GameObject notebookOpen;

    [HideInInspector]
    public bool truckIsOpen = false;

    void Start()
    {
        if (truckOpen != null) truckOpen.SetActive(false);
        if (notebookClosed != null) notebookClosed.SetActive(false);
        if (notebookOpen != null) notebookOpen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.gameObject == gameObject)
            {
                OpenTruck();
            }
            else if (truckIsOpen && (hit == null || (hit.gameObject != notebookClosed && hit.gameObject != notebookOpen)))
            {
                CloseTruck();
            }
        }
    }

    void OpenTruck()
    {
        if (truckOpen != null) truckOpen.SetActive(true);
        if (notebookClosed != null) notebookClosed.SetActive(true);
        truckIsOpen = true;
    }

    void CloseTruck()
    {
        if (truckOpen != null) truckOpen.SetActive(false);
        if (notebookClosed != null) notebookClosed.SetActive(false);
        if (notebookOpen != null) notebookOpen.SetActive(false);
        truckIsOpen = false;
    }
}