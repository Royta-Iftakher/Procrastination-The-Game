using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    public static Phone Instance { get; private set; }
    public GameObject phoneObject; // The phone object with the RectTransform you want to manipulate
    private float hoverMoveDistance = 600f;
    private Vector3 originalPosition;
    private Vector3 phoneOutPosition;

    private bool phonePulled = false;
    private bool inCall;
    private bool messages;

    private RectTransform rectTransform; // The RectTransform of the phone object

    public GameObject endCallButton;
    public Slider callSlider;
    public Image phoneImage;

    public Sprite[] phoneSprites;

    public GameObject transcriptObject; // The transcript object to enable/disable

    private void Awake() // The Singleton pattern
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        rectTransform = phoneObject.GetComponent<RectTransform>(); // Get the RectTransform from the phone object

        // Set the anchor and pivot to bottom-right
        rectTransform.anchorMin = new Vector2(.90f, 0);  // bottom-right
        rectTransform.anchorMax = new Vector2(.90f, 0);  // bottom-right
        rectTransform.pivot = new Vector2(.90f, 0);  // bottom-right

        // Update original position after setting anchor and pivot
        originalPosition = rectTransform.anchoredPosition;

        // Adjust hoverMoveDistance relative to screen height
        phoneOutPosition = new Vector3(originalPosition.x, originalPosition.y + hoverMoveDistance, originalPosition.z);

        phoneImage.sprite = phoneSprites[0];
        callSlider.onValueChanged.AddListener(OnSliderValueChanged);
        endCallButton.SetActive(false);
        transcriptObject.SetActive(false); // Disable the transcript object initially

        // Hide the phone at start
        phoneObject.SetActive(false);
    }


    private void Update()
    {
        if (!phonePulled && PauseMenu.Instance.emailsDone)
        {
            // Show the phone when emails are done
            phoneObject.SetActive(true);
            AudioManager.instance.PhoneRinging(); // Play phone ringing sound
        }

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

        //if(!inCall &&  GameManager.Instance.phoneAnswered = true;) {   
        //}
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
        // Check if the phone is currently open
        if (phonePulled)
        {
            // If the slider's value is 1 and there's no ongoing call
            if (value == 1 && !inCall)
            {
                AudioManager.instance.StopPhoneRinging();
                AudioManager.instance.PhoneCall();
                Debug.Log("The slider's value is 1. A function might be called here later.");
                inCall = true;

                if (phoneSprites.Length > 1)
                {
                    phoneImage.sprite = phoneSprites[1];  // Set to second sprite as soon as the slider is swiped
                }

                // Hide the slider
                callSlider.gameObject.SetActive(false);
                endCallButton.SetActive(true);
                GameManager.Instance.phoneAnswered = true;

                // Enable the transcript object
                transcriptObject.SetActive(true);

                // Stop the phone from ringing
                

                // Start the coroutine to wait for the sound to finish
                StartCoroutine(WaitForSound(AudioManager.instance.GetCurrentClipLength()));
            }
        }
        else
        {
            // Reset the slider value to 0 if the phone is not pulled
            callSlider.value = 0;
        }
    }




    private IEnumerator WaitForSound(float length)
    {
        // Wait for the length of the sound
        yield return new WaitForSeconds(length);

        endCallButton.SetActive(false);
        transcriptObject.SetActive(false);
        // Change the image to the third sprite after the sound finishes
        if(phoneSprites.Length > 2) {
            phoneImage.sprite = phoneSprites[2];
        }
        
        // Call is over
        inCall = false;
    }

    public void DestroyInstance()
    {
        // Nullify the static instance
        // Destroy the game object this script is attached to
        Destroy(this.gameObject);
    }

    private void GoToMessages()
    {
        if(phoneSprites.Length > 3) {
            phoneImage.sprite = phoneSprites[3]; // Set to fourth sprite for messages
            GameManager.Instance.phoneFinished = true;
        }
    }

    public void EndCall()
    {
        AudioManager.instance.ButtonClick();
        AudioManager.instance.StopSound(AudioManager.instance.friendTalking); // Assuming you have a method to stop the current audio clip
        phoneImage.sprite = phoneSprites[2];
        inCall = false;
        //GoToMessages();
        GameManager.Instance.phoneFinished = true;
        // Disable the transcript object
        endCallButton.SetActive(false);
        transcriptObject.SetActive(false);
    }

}
