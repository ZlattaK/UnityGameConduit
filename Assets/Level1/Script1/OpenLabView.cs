using UnityEngine;

public class OpenLabView : MonoBehaviour
{
    public GameObject labArea;    
    public GameObject itemA;      
    public GameObject itemB;      

    private bool isOpen = false;

    void Start()
    {
        if (labArea != null)
            labArea.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouse, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                ToggleLab();
            }
            else if (isOpen)
            {
                CloseLab();
            }
        }
    }

    private void ToggleLab()
    {
        if (isOpen) CloseLab();
        else OpenLab();
    }

    private void OpenLab()
    {
        isOpen = true;

        labArea.SetActive(true);

        if (itemA != null) itemA.SetActive(!CombineItems.hasCrafted);
        if (itemB != null) itemB.SetActive(!CombineItems.hasCrafted);
    }

    private void CloseLab()
    {
        isOpen = false;

        labArea.SetActive(false);

        if (itemA != null) itemA.SetActive(false);
        if (itemB != null) itemB.SetActive(false);
    }
}
