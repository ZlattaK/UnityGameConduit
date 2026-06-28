using UnityEngine;
using System.Collections;

public class Sprout : MonoBehaviour
{
    public PlantController plantController;

    [Header("Fade settings")]
    public float fadeDuration = 0.5f;

    private bool isDestroyed = false;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            if (isDestroyed) return;

            isDestroyed = true;

            if (plantController != null)
                plantController.RegisterSproutDestroyed();

            StartCoroutine(FadeAndDisable());
        }
    }

    IEnumerator FadeAndDisable()
    {
        float t = 0f;

        Color startColor = sr.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            sr.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            yield return null;
        }

        gameObject.SetActive(false);
    }
}