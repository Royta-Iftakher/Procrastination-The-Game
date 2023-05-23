using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Import this if the Yes and No options are TextMeshProUGUI

public class Prompt : MonoBehaviour
{

    // The buttons for the Yes and No options
    public Button yesButton;
    public Button noButton;

    // The TextMeshProUGUI components for the Yes and No options
    public TextMeshProUGUI yesOptionText;
    public TextMeshProUGUI noOptionText;

    // The lists of possible Yes and No options
    private string[] yesOptions = { 
    "I'm going to do this",
    "Better get on it",
    "Just Do It!",
    "No time like the present",
    "Start now!",
    "I should probably start",
    "Okay, let's do this",
    "I'll tackle it head-on",
    "Time to face the challenge",
    "I'll feel relieved once it's done"
};

private string[] noOptions = {
    "Nevermind",
    "My favorite show starts in 5 minutes!",
    "Just 5 more minutes...",
    "I'll do it tomorrow",
    "Maybe later",
    "I can do it later when I have more energy",
    "I'll wait for the right mood",
    "I'm not feeling up to it right now",
    "There's still plenty of time",
    "I work better under pressure"
};


    private System.Random rand = new System.Random();
    
    void Start()
    {
        if (gameObject.tag != "Prompt")
        {
            // If this GameObject doesn't have the "Prompt" tag, disable this script
            this.enabled = false;
        }
        else
        {
            // If this GameObject does have the "Prompt" tag, hide the prompt initially
            hidePrompt();
        }
    }


    public void goToScene()
    {
        hidePrompt();
        GameManager.Instance.inTask = true;
        GameManager.Instance.sceneLoader();
        SceneManager.LoadScene(GameManager.Instance.sceneName, LoadSceneMode.Additive);
    }

    public void cancel()
    {
        hidePrompt();
    }

    public void hidePrompt()
    {
        if(yesButton.gameObject != null) {
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        }
    }

    public void showPrompt()
    {
        if(!GameManager.Instance.socialSwitch) {
        // Choose random Yes and No options
            string yesOption = yesOptions[rand.Next(yesOptions.Length)];
            string noOption = noOptions[rand.Next(noOptions.Length)];

            // Update the Yes and No option text
            yesOptionText.text = yesOption;
            noOptionText.text = noOption;
        }

        // Show the buttons
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
    }

    void Update()
    {

    }
}
