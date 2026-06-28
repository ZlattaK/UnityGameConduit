using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverHighlight : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Color originalColor;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    void OnMouseEnter()
    {
        sr.color = highlightColor;
    }

    void OnMouseExit()
    {
        sr.color = originalColor;
    }
}
