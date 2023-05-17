using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required when using Event data
using UnityEngine.UI; // Required when Using UI elements.

public class Phone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float hoverMoveDistance = 10f;
    private Vector3 originalPosition;

    private RectTransform rectTransform;

    private void Start() 
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.position;
    }

    // When the mouse enters the phone
    public void OnPointerEnter(PointerEventData eventData) 
    {
        rectTransform.position += new Vector3(0, hoverMoveDistance, 0);
    }

    // When the mouse exits the phone
    public void OnPointerExit(PointerEventData eventData) 
    {
        rectTransform.position = originalPosition;
    }

    // When the phone is clicked
    public void OnPointerClick(PointerEventData eventData) 
    {
        rectTransform.position = new Vector3(rectTransform.position.x, originalPosition.y + 575f, rectTransform.position.z);
    }

    public void OnCloseButtonClicked() 
    {
        rectTransform.position = originalPosition;
    }

}
