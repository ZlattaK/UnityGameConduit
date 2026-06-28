using UnityEngine;
using System.Collections;

public class SimpleBoxController : MonoBehaviour
{
    [Header("Overlay картинка")]
    public GameObject overlayImage;

    [Header("Overlay задержка")]
    public float overlayDelay = 0.3f;

    [Header("Открытый ящик")]
    public GameObject boxOpen;

    [Header("Пароль UI")]
    public GameObject passwordPanel;

    [Header("Файл")]
    public PasswordFileSpawner fileSpawner;

    private bool isOpen = false;
    private Coroutine overlayCoroutine;

    void Start()
    {
        if (boxOpen != null)
            boxOpen.SetActive(false);

        if (passwordPanel != null)
            passwordPanel.SetActive(false);

        if (overlayImage != null)
            overlayImage.SetActive(false);
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
                    OpenBox();
            }
            else
            {
                if (isOpen)
                    CloseBox();
            }
        }
    }

    void OpenBox()
    {
        if (boxOpen != null)
            boxOpen.SetActive(true);

        if (passwordPanel != null)
            passwordPanel.SetActive(true);

        isOpen = true;

        if (overlayCoroutine != null)
            StopCoroutine(overlayCoroutine);

        overlayCoroutine = StartCoroutine(ShowOverlayWithDelay(overlayDelay));

        if (fileSpawner != null)
            fileSpawner.CreateFile();
    }

    void CloseBox()
    {
        if (boxOpen != null)
            boxOpen.SetActive(false);

        if (passwordPanel != null)
            passwordPanel.SetActive(false);

        if (overlayImage != null)
            overlayImage.SetActive(false);

        if (overlayCoroutine != null)
            StopCoroutine(overlayCoroutine);

        isOpen = false;
    }

    IEnumerator ShowOverlayWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (overlayImage != null && isOpen)
            overlayImage.SetActive(true);
    }
}