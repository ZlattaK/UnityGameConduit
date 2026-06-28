using UnityEngine;
using System.Collections;

public class ArrowCamera : MonoBehaviour
{
    [Header("Камера")]
    public Camera targetCamera;

    [Header("Координаты")]
    public Vector3 targetPosition = new Vector3(98.41f, 0.39f, -10f);

    [Header("Новая локация")]
    public ItemLocationHandler.Location newLocation;

    private bool isTransitioning = false;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!isTransitioning)
                StartCoroutine(MoveCameraRoutine());
        }
    }

    IEnumerator MoveCameraRoutine()
    {
        isTransitioning = true;

        yield return StartCoroutine(FadeManager.Instance.FadeOut());

        if (targetCamera == null)
            targetCamera = Camera.main;

        targetCamera.transform.position = targetPosition;

        ItemLocationHandler[] items = FindObjectsOfType<ItemLocationHandler>();

        foreach (var item in items)
        {
            if (item != null)
            {
                item.currentLocation = newLocation;
                Debug.Log("Переключили предмет: " + item.name + " -> " + newLocation);
            }
        }

        yield return new WaitForSeconds(0.1f);

        yield return StartCoroutine(FadeManager.Instance.FadeIn());

        isTransitioning = false;
    }
}