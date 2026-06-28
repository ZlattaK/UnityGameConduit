using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public Transform target;

    [Header("Ссылка на ящик")]
    public BoxController boxController;

    private Vector3 startPos;
    private bool placed = false;

    void Start()
    {
        startPos = transform.position;
    }

    void OnMouseDrag()
    {
        if (placed) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

    void OnMouseUp()
    {
        if (placed) return;

        transform.position = target.position;
        placed = true;

        Debug.Log("Placed: " + gameObject.name);

        if (boxController != null)
        {
            boxController.RegisterPiecePlaced();
        }
    }
}