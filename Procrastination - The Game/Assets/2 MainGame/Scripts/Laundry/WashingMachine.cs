using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WashingMachine : MonoBehaviour
{
    public float laundryTime = 30f; // The time it takes for the laundry to complete
    public TextMeshProUGUI timerText; // Assign the TextMeshProUGUI component in the Inspector
    public Button startButton; // Assign the button in the Inspector
    public Color normalColor = Color.white; // The normal color of the button
    public Color disabledColor = Color.gray; // The color of the button when the laundry is done
    public TextMeshProUGUI warningText; // Assign a TextMeshProUGUI component to display warning messages
    public LaundryBasket laundryBasket; // Assign the LaundryBasket component in the Inspector
    public float warningDisplayTime = 3f; // The time (in seconds) to display the warning message

    private float timer;
    private float warningTimer = 0f; // Timer for the warning message
    public bool isLaundryDone = false; // Initially, the laundry is not done
    private bool isLaundryStarted = false; // Initially, the laundry has not started

    void Start()
    {
        timerText.text = ""; // Hide the timer at the start
        warningText.text = ""; // Hide the warning text at the start
        startButton.onClick.AddListener(StartLaundry);
        startButton.image.color = normalColor; // Set the normal color of the button
    }

    void Update()
    {
        if (isLaundryStarted && !isLaundryDone)
        {
            timer -= Time.deltaTime;

            // Update the timer text
            timerText.text = Mathf.Ceil(timer).ToString();

            if (timer <= 0)
            {
                isLaundryDone = true; // The laundry is done
                timerText.text = ""; // Hide the timer when the laundry is done
                // Don't enable the button again, it can only be clicked once
                startButton.image.color = disabledColor; // Set the disabled color of the button
            }
        }

        // Update the warning timer
        if (warningTimer > 0)
        {
            warningTimer -= Time.deltaTime;
            if (warningTimer <= 0)
            {
                warningText.text = ""; // Hide the warning text when the time is up
            }
        }
    }

    public void StartLaundry()
    {
        // Only start the laundry if it's not already running and the laundry basket is being carried
        if (!isLaundryDone && laundryBasket.basketCarried)
        {
            isLaundryStarted = true;
            timer = laundryTime;
            startButton.interactable = false; // Disable the button while the laundry is running
            startButton.image.color = normalColor; // Set the normal color of the button
            warningText.text = ""; // Hide the warning text
        }
        else
        {
            warningText.text = "You don't have the laundry basket!";
            warningTimer = warningDisplayTime; // Set the warning timer
        }
    }
}
