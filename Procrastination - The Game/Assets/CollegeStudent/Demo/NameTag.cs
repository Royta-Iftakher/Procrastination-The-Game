using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameTag : MonoBehaviour
{
    public Transform target; // The target GameObject to follow (Player)
    public Vector3 offset; // Offset position of the NameTag from the target GameObject
    private Camera mainCamera;
    private RectTransform rectTransform;
    public TextMeshProUGUI nameTxt;

    private void Start()
    {
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
        setNameText();
    }

    void Update()
    {
        if (target)
        {
            // Convert the world position of the target GameObject to screen position
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(target.position + offset);
            // Set the position of the NameTag RectTransform to the screen position
            rectTransform.position = screenPosition;
        }
    }

    public void setNameText()
    {
        nameTxt.text = DataManager.Instance.GetName();
    }

    public void AdjustOffset(Vector3 newOffset){
        offset = newOffset;
    }
}
