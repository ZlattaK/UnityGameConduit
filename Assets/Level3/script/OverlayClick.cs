using UnityEngine;
using UnityEngine.EventSystems;

public class OverlayClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject overlay;

    public void OnPointerClick(PointerEventData eventData)
    {
        overlay.SetActive(false);
    }
}