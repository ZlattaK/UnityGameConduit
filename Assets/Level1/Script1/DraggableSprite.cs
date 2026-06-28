using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DraggableSprite : MonoBehaviour
{
    [Header("Inventory Slots")]
    public InventorySlot[] slots;

    [Header("Настройки")]
    public float snapDistance = 0.5f;

    private Camera mainCamera;
    private Vector3 offset;
    private Vector3 startPosition;
    public InventorySlot currentSlot = null;
    private bool isDragging = false;
    private bool justRemovedFromSlot = false;
    private ItemLocationHandler locationHandler;

    public bool wasJustTakenOut = false;

    private void Awake()
    {
        mainCamera = FindObjectOfType<Camera>(); ;
        startPosition = transform.position;

        locationHandler = GetComponent<ItemLocationHandler>();

        if (slots == null || slots.Length == 0)
            Debug.LogWarning("[Drag] Массив слотов пуст!");
        else
            Debug.Log("[Drag] Привязано слотов вручную: " + slots.Length);
    }

    private void OnMouseDown()
    {
        if (currentSlot != null)
        {
            currentSlot.isOccupied = false;
            transform.SetParent(null);
            currentSlot = null;

            if (locationHandler != null)
            {
                transform.position = locationHandler.GetPositionForCurrentLocation();
                startPosition = transform.position;
            }
            else
            {
                transform.position = startPosition;
            }

            wasJustTakenOut = true;

            isDragging = false;
            justRemovedFromSlot = true;
            Debug.Log("CLICK DETECTED");
            Debug.Log("[Drag] Предмет возвращен на старт");
            return;
        }

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;
        offset = transform.position - mouseWorld;
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;
        transform.position = mouseWorld + offset;
    }

    private void OnMouseUp()
    {
        if (justRemovedFromSlot)
        {
            justRemovedFromSlot = false;
            return; 
        }

        if (!isDragging) return;
        isDragging = false;

        Vector2 mousePos2D = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        InventorySlot targetSlot = null;

        foreach (InventorySlot slot in slots)
        {
            if (slot == null || slot.isOccupied) continue;

            Collider2D col = slot.GetComponent<Collider2D>();
            if (col != null && col.OverlapPoint(mousePos2D))
            {
                targetSlot = slot;
                break;
            }
        }

        if (targetSlot != null)
        {
            transform.position = targetSlot.transform.position;
            transform.SetParent(targetSlot.transform); 
            targetSlot.isOccupied = true;
            currentSlot = targetSlot;
            Debug.Log("[Drag] Нашёл слот: " + targetSlot.name);
        }
        else
        {
            transform.position = startPosition;
            currentSlot = null;
        }
    }
}
