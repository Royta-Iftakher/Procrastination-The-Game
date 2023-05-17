using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndNote : MonoBehaviour
{
    public TextMeshProUGUI endNoteText;
    public TextMeshProUGUI gradeText; // Text component to display the grade
    public TextMeshProUGUI timeText; // Text component to display the time left

    private string[] gradeStrings = { "A+", "A", "B", "C", "D", "F" }; // Grade strings

    public float typingSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        SetEndNoteAndGrade(Score.Instance.letterGrade);
        StartCoroutine(TypeOutTextCoroutine(endNoteText, endNoteText.text));
        SetTimeLeft();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEndNoteAndGrade(int gradeIndex)
    {
        if(gradeIndex >= 0 && gradeIndex < gradeStrings.Length)
        {
            gradeText.text = gradeStrings[gradeIndex];

            switch (gradeIndex)
            {
                case 0: // A+
                    endNoteText.text = "You have transcended procrastination. Please teach us your ways!";
                    break;
                case 1: // A
                    endNoteText.text = "You're the kind of person who does their taxes in January, aren't you?";
                    break;
                case 2: // B
                    endNoteText.text = "Not bad! You only checked social media twice.";
                    break;
                case 3: // C
                    endNoteText.text = "You might've watched one too many cat videos. But who can blame you?";
                    break;
                case 4: // D
                    endNoteText.text = "Did you have a Netflix tab open the whole time?";
                    break;
                case 5: // F
                    endNoteText.text = "Are you the CEO of Procrastination?";
                    break;
            }
        }
        else
        {
            Debug.LogError("Invalid grade index.");
        }
    }


    public void SetTimeLeft()
    {
        string timeLeft = GameTimer.Instance.GetTimeLeft();
        if(timeLeft != "You ran out of time") {
            timeText.text = "Time Left: " + timeLeft;
        }
        else {
            timeText.text = "You ran out of time";
        }
    }

    IEnumerator TypeOutTextCoroutine(TextMeshProUGUI textComponent, string targetText)
    {
        textComponent.text = string.Empty;

        foreach (char letter in targetText)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(1f / typingSpeed);
        }
    }

}

//find a way to make it like the player is writing out the scores
//include writing audio
