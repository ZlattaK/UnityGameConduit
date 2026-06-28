using UnityEngine;

public class NotebookController : MonoBehaviour
{
    public GameObject notebookOpen;       
    public TruckController truckController; 

    private bool notebookIsOpen = false;

    void Start()
    {
        if (notebookOpen != null)
            notebookOpen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.gameObject == gameObject && truckController != null && truckController.truckIsOpen)
            {
                notebookOpen.SetActive(true);
                notebookIsOpen = true;
            }
            else if (notebookIsOpen && (hit == null || (hit.gameObject != gameObject && hit.gameObject != notebookOpen)))
            {
                notebookOpen.SetActive(false);
                notebookIsOpen = false;
            }
        }
    }
}