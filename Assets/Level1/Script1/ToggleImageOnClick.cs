using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleImageOnRightClick : MonoBehaviour
{
    [Header("Картинка для просмотра")]
    public GameObject targetImage; 
    private bool isVisible = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isVisible = !isVisible;
                targetImage.SetActive(isVisible);
            }
            else
            {
                if (isVisible)
                {
                    isVisible = false;
                    targetImage.SetActive(false);
                }
            }
        }
    }
}
