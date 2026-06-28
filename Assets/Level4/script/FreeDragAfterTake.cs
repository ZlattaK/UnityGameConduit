using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FreeDragAfterTake : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    private bool isDragging = false;

    private DraggableSprite draggable;

    void Awake()
    {
        mainCamera = Camera.main;
        draggable = GetComponent<DraggableSprite>();
    }

    void OnMouseDown()
    {
        if (draggable != null && draggable.currentSlot != null)
            return;

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;

        offset = transform.position - mouseWorld;
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;

        transform.position = mouseWorld + offset;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}