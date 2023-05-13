using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistractionPopup : MonoBehaviour
{
    public GameObject[] popups;  // Array of gameobject popups
    public float delay = 10f;  // Time delay before the distraction appears
    public float duration = 5f;  // Time that the distraction will remain on screen
    [SerializeField] AudioSource audio;
    [SerializeField] private Timer timerScript;

    private float timer;  // Timer to keep track of when the distraction should appear

    void Start()
    {
        GameObject timerObject = GameObject.Find("TimeManager");
        timerScript = timerObject.GetComponent<Timer>();
        timer = delay;  // Start the timer at the delay time
    }

    void Update()
    {
        timer -= Time.deltaTime;  // Subtract time from the timer

        if (timer <= 0 && (timerScript.currentTime != 0))
        {
            // Show a random pop-up from the array
            int index = Random.Range(0, popups.Length);
            GameObject selectedPopup = popups[index];
            AudioSource.PlayClipAtPoint(audio.clip, transform.position);
            selectedPopup.SetActive(true);

            // Start a coroutine to hide the pop-up after a set duration
            StartCoroutine(HidePopup());

            // Reset the timer for the next pop-up
            timer = Random.Range(5f, 15f);
        }
    }

    IEnumerator HidePopup()
    {
        // Wait for the specified duration before hiding the pop-up
        yield return new WaitForSeconds(duration);

        // Hide the active pop-up
        foreach (GameObject popup in popups) {
            if (popup.activeSelf) {
                popup.SetActive(false);
                break;
            }
        }
    }
}
