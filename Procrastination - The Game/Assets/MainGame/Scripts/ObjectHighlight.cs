using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlight : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Color originalColor;
   
    

    private void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
    }

    private void OnMouseEnter()
    {
       
        GetComponent<Renderer>().material.color = highlightColor;
    }

    private void OnMouseExit()
    {
        
        GetComponent<Renderer>().material.color = originalColor;
    }

}
