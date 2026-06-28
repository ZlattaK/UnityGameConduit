using UnityEngine;
using System.Collections;

public class LastCamera : MonoBehaviour
{
    [Header("Камера")]
    public Camera targetCamera;

    [Header("Куда перемещаем камеру")]
    public Vector3 targetPosition;

    [Header("Задержка перед переходом")]
    public float delayBeforeMove = 1f;

    [Header("Диалог")]
    public DialogController dialogController;
    [TextArea] public string[] dialogLines;

    [Header("Задержка перед диалогом")]
    public float delayBeforeDialog = 1f;

    private bool isTransitioning;

    public void StartTransition()
    {
        if (!isTransitioning)
            StartCoroutine(TransitionRoutine());
    }

    private IEnumerator TransitionRoutine()
    {
        isTransitioning = true;

        yield return StartCoroutine(FadeManager.Instance.FadeOut());

        yield return new WaitForSeconds(delayBeforeMove);

        if (targetCamera == null)
            targetCamera = Camera.main;

        targetCamera.transform.position = targetPosition;

        yield return new WaitForSeconds(0.1f);

        yield return StartCoroutine(FadeManager.Instance.FadeIn());

        yield return new WaitForSeconds(delayBeforeDialog);

        if (dialogController != null && dialogLines != null && dialogLines.Length > 0)
        {
            dialogController.StartDialog(dialogLines);
        }

        isTransitioning = false;
    }
}