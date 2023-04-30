using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float hours = 10.0f;
    private float minutes = 0.0f;
    private float timeScale = 60.0f; // 1 real second = 1 in-game minute
    private float timeElapsed = 0.0f;
    private float dayDurationInSeconds = 720.0f * 60.0f; // 12 hours x 60 minutes x 60 seconds

    void Start()
    {
        // Set the initial time in the text
        UpdateTimeText();
    }

    void Update()
    {
        // Calculate the time elapsed since the last frame
        float deltaTime = Time.deltaTime * timeScale;

        // Update the time based on the elapsed time
        timeElapsed += deltaTime;
        minutes = (timeElapsed / 60.0f) % 60.0f;
        hours = 10.0f + (timeElapsed / 3600.0f) % 12.0f;

        // Update the time text
        UpdateTimeText();
    }

    void UpdateTimeText()
    {
        string timeString = string.Format("{0:00}:{1:00} {2}", 
            hours == 12.0f ? 12 : hours % 12, (int)minutes, hours < 12.0f ? "AM" : "PM");
        timeText.text = timeString;

        // Reset the time elapsed if a day has passed
        if (timeElapsed >= dayDurationInSeconds)
        {
            timeElapsed = 0.0f;
        }
    }
}
