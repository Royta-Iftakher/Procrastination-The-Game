using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlight : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Color originalColor;
    private bool isMouseOver = false;

    private void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;
        GetComponent<Renderer>().material.color = highlightColor;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
        GetComponent<Renderer>().material.color = originalColor;
    }

}
