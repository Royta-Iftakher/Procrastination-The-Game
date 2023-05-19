using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Phone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float hoverMoveDistance = 10f;
    private Vector3 originalPosition;
    private Vector3 phoneOutPosition;

    private bool phonePulled = false;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.position;
        phoneOutPosition = new Vector3(originalPosition.x, originalPosition.y + 575f, originalPosition.z);
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

    // When the mouse enters the phone
    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(MovePhone(originalPosition + new Vector3(0, hoverMoveDistance, 0), 0.25f));
    }

    // When the mouse exits the phone
    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(MovePhone(originalPosition, 0.25f));
    }

    // When the phone is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        OpenPhone();
    }

    public void OnCloseButtonClicked()
    {
        ClosePhone();
    }

    private void OpenPhone()
    {
        StopAllCoroutines();
        StartCoroutine(MovePhone(phoneOutPosition, 0.25f));
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
        Vector3 startPosition = rectTransform.position;

        while (time < duration)
        {
            time += Time.deltaTime;
            rectTransform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            yield return null;
        }

        rectTransform.position = targetPosition;
    }
}
