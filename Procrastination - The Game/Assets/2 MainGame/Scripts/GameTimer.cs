using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }

    public string timeLeft;
    [SerializeField] private TextMeshProUGUI timeText;
    private int hours = 10;
    private int minutes = 0;
    private int timeScale = 60; // 1 real second = 1 in-game minute
    private float timeElapsed = 0.0f;
    private float dayDurationInSeconds = 720.0f * 60.0f; // 12 hours x 60 minutes x 60 seconds

    void Awake()
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
        DisableChildren(transform);
    }

    void Start()
    {
        // Set the initial time in the text
        UpdateTimeText();
    }

    void Update()
    {
        if(GameManager.Instance.gameStarted == true) 
        {
            // Calculate the time elapsed since the last frame
            float deltaTime = Time.deltaTime * timeScale;

            // Update the time based on the elapsed time
            timeElapsed += deltaTime;
            minutes = (int)(timeElapsed / 60.0f) % 60;
            hours = (10 + (int)(timeElapsed / 3600.0f)) % 12;

            // Update the time text
            UpdateTimeText();

            float timeLeftInSeconds = dayDurationInSeconds - timeElapsed;
            int hoursLeft = (int)(timeLeftInSeconds / 3600.0f);
            int minutesLeft = (int)((timeLeftInSeconds % 3600.0f) / 60.0f);
            timeLeft = string.Format("{0:00}:{1:00}", hoursLeft, minutesLeft);
        }
    }

    void UpdateTimeText()
    {
        int totalHours = 10 + (int)(timeElapsed / 3600.0f);
        string amPm = totalHours >= 12 && totalHours < 24 ? "PM" : "AM";
        string timeString = string.Format("{0:00}:{1:00} {2}",
            hours == 0 ? 12 : hours, minutes, amPm);
        timeText.text = timeString;

        // Reset the time elapsed if a day has passed
        if (timeElapsed >= dayDurationInSeconds)
        {
            timeElapsed = 0.0f;
        }
    }

    public void ResetTimer()
    {
        hours = 10;
        minutes = 0;
        timeElapsed = 0.0f;
    }

    public void DisableChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(false);
            if(child.childCount > 0)
            {
                DisableChildren(child);
            }
        }
    }

    public void EnableChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(true);
            if(child.childCount > 0)
            {
                EnableChildren(child);
            }
        }
    }

    public void EnableChildren() {
        EnableChildren(transform);
    }
    public void DisableChildren() {
        DisableChildren(transform);
    }

}
