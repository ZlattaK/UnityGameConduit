using UnityEngine;

public class DragWire : MonoBehaviour
{
    private Vector3 offset;
    private bool dragging;

    void OnMouseDown()
    {
        dragging = true;

        Vector3 mouse =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        offset = transform.position - new Vector3(mouse.x, mouse.y, 0f);
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    void Update()
    {
        if (dragging)
        {
            Vector3 mouse =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position =
                new Vector3(mouse.x, mouse.y, 0f) + offset;
        }
    }
}