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

    public Slider callSlider;
    public Image phoneImage;

    public Sprite[] phoneSprites;

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

        callSlider.onValueChanged.AddListener(OnSliderValueChanged);
        if(GameManager.Instance.phoneAnswered == true) {
            callSlider.gameObject.SetActive(false);
            phoneImage.sprite = phoneSprites[2];
        }
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

    private void OnSliderValueChanged(float value)
    {
        // If the slider's value is 1
        if(value == 1)
        {
            AudioManager.instance.PhoneCall();
            Debug.Log("The slider's value is 1. A function might be called here later.");

            if(phoneSprites.Length > 1) {
                phoneImage.sprite = phoneSprites[1];  // Set to second sprite as soon as the slider is swiped
            }

            // Hide the slider
            callSlider.gameObject.SetActive(false);
            GameManager.Instance.phoneAnswered = true;

            // Start the coroutine to wait for the sound to finish
            StartCoroutine(WaitForSound(AudioManager.instance.GetCurrentClipLength()));
        }
    }

    private IEnumerator WaitForSound(float length)
    {
        // Wait for the length of the sound
        yield return new WaitForSeconds(length);

        // Change the image to the third sprite after the sound finishes
        if(phoneSprites.Length > 2) {
            phoneImage.sprite = phoneSprites[2];
        }
    }
}
