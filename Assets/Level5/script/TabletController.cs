using UnityEngine;

public class TabletController : MonoBehaviour
{
    [Header("Открытый планшет")]
    public GameObject tabletOpen;

    private bool isOpen = false;

    void Start()
    {
        if (tabletOpen != null)
            tabletOpen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.gameObject == gameObject)
            {
                if (!isOpen)
                    OpenTablet();
            }
            else
            {
                if (isOpen)
                    CloseTablet();
            }
        }
    }

    void OpenTablet()
    {
        if (tabletOpen != null)
            tabletOpen.SetActive(true);

        isOpen = true;
    }

    void CloseTablet()
    {
        if (tabletOpen != null)
            tabletOpen.SetActive(false);

        isOpen = false;
    }
}