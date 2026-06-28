using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DragItem : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private bool dragging = false;

    void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        dragging = true;
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;
        offset = transform.position - mouseWorld;
    }

    void OnMouseDrag()
    {
        if (!dragging) return;
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;
        transform.position = mouseWorld + offset;
    }

    void OnMouseUp()
    {
        dragging = false;
    }
}
