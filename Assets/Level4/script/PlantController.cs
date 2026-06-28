using UnityEngine;
using System.Collections;

public class PlantController : MonoBehaviour
{
    [Header("Главное растение")]
    public GameObject mainPlant;

    [Header("Всего ростков")]
    public int totalSprouts = 3;

    [Header("Эффекты")]
    public float pulseDuration = 0.5f;
    public float fadeDuration = 0.5f;
    public float pulseScale = 1.01f;

    private int destroyedSprouts = 0;
    private bool isBlocked = true;
    private bool isUnlocking = false;

    public bool IsBlocked()
    {
        return isBlocked;
    }

    public void RegisterSproutDestroyed()
    {
        destroyedSprouts++;

        Debug.Log("Уничтожено ростков: " + destroyedSprouts + " / " + totalSprouts);

        if (destroyedSprouts >= totalSprouts && !isUnlocking)
        {
            UnlockPlant();
        }
    }

    void UnlockPlant()
    {
        isBlocked = false;
        isUnlocking = true;

        Debug.Log("Растение уничтожено, запускаем эффект");

        if (mainPlant != null)
        {
            StartCoroutine(PlantDeathEffect(mainPlant));
        }
    }

    IEnumerator PlantDeathEffect(GameObject plant)
    {
        Transform tr = plant.transform;
        Vector3 originalScale = tr.localScale;

        SpriteRenderer sr = plant.GetComponent<SpriteRenderer>();

        float t = 0f;
        while (t < pulseDuration)
        {
            t += Time.deltaTime;

            float pulse = 1f + Mathf.Sin(t * 20f) * 0.05f;
            tr.localScale = originalScale * pulse;

            yield return null;
        }

        tr.localScale = originalScale * pulseScale;

        if (sr != null)
        {
            Color startColor = sr.color;
            float f = 0f;

            while (f < fadeDuration)
            {
                f += Time.deltaTime;

                float alpha = Mathf.Lerp(1f, 0f, f / fadeDuration);
                sr.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

                yield return null;
            }
        }

        plant.SetActive(false);

        Debug.Log("Растение исчезло полностью");
    }
}