using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverHighlight2 : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Color originalColor;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            originalColor = sr.color;
    }

    void OnMouseEnter()
    {
        if (sr != null)
            sr.color = highlightColor;
    }

    void OnMouseExit()
    {
        if (sr != null)
            sr.color = originalColor;
    }
}