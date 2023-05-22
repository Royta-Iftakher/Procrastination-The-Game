using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    private float hoverMoveDistance = 600f;
    private Vector3 originalPosition;
    private Vector3 phoneOutPosition;

    private bool phonePulled = false;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        
        // Set the anchor and pivot to bottom-right
        rectTransform.anchorMin = new Vector2(.90f, 0);  // bottom-right
        rectTransform.anchorMax = new Vector2(.90f, 0);  // bottom-right
        rectTransform.pivot = new Vector2(.90f, 0);  // bottom-right

        // Update original position after setting anchor and pivot
        originalPosition = rectTransform.anchoredPosition;

        // Adjust hoverMoveDistance relative to screen height
        phoneOutPosition = new Vector3(originalPosition.x, originalPosition.y + hoverMoveDistance, originalPosition.z);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(phonePulled)
            {
                ClosePhone();
            }
            else
            {
                OpenPhone();
            }
        }
    }

    private void OpenPhone()
    {
        StopAllCoroutines();
        StartCoroutine(MovePhone(new Vector3(originalPosition.x, originalPosition.y + hoverMoveDistance, originalPosition.z), 0.25f)); 
        phonePulled = true;
    }

    private void ClosePhone()
    {
        StopAllCoroutines();
        StartCoroutine(MovePhone(originalPosition, 0.25f));
        phonePulled = false;
    }

    IEnumerator MovePhone(Vector3 targetPosition, float duration) 
    {
        float time = 0;
        Vector3 startPosition = rectTransform.anchoredPosition;

        while (time < duration)
        {
            time += Time.deltaTime;
            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;
    }
}
