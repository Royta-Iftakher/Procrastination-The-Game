using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public const int NUM_HIGH_SCORES = 5;
    public const string NAME_KEY = "HSName";
    public const string SCORE_KEY = "HScore";
    public const string TIME_KEY = "HTime"; // Add this line

    [SerializeField] string playerName;
    [SerializeField] int playerScore;
    [SerializeField] float timeLeft; // Add this line

    [SerializeField] TextMeshProUGUI[] nameTexts;
    [SerializeField] TextMeshProUGUI[] scoreTexts;
    [SerializeField] TextMeshProUGUI[] timeTexts; // Add this line

    // Start is called before the first frame update
    void Start()
    {
        playerName = DataManager.Instance.GetName();
        playerScore = DataManager.Instance.GetScore();
        timeLeft = GameTimer.Instance.GetTimeLeftFloat();

        SaveScore();
        DisplayHighScores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveScore()
    {
        for (int i = 1; i <= NUM_HIGH_SCORES; i++)
        {
            string currentNameKey = NAME_KEY + i;
            string currentScoreKey = SCORE_KEY + i;
            string currentTimeKey = TIME_KEY + i; // Add this line

            if (PlayerPrefs.HasKey(currentScoreKey))
            {
                int currentScore = PlayerPrefs.GetInt(currentScoreKey);
                float currentTime = PlayerPrefs.GetFloat(currentTimeKey); // Add this line

                if (playerScore > currentScore || timeLeft > currentTime) // Modify this line
                {
                    int tempScore = currentScore;
                    string tempName = PlayerPrefs.GetString(currentNameKey);
                    float tempTime = currentTime; // Add this line

                    PlayerPrefs.SetString(currentNameKey, playerName);
                    PlayerPrefs.SetInt(currentScoreKey, playerScore);
                    PlayerPrefs.SetFloat(currentTimeKey, timeLeft); // Add this line

                    playerName = tempName;
                    playerScore = tempScore;
                    timeLeft = tempTime; // Add this line
                }
            }
            else
            {
                PlayerPrefs.SetString(currentNameKey, playerName);
                PlayerPrefs.SetInt(currentScoreKey, playerScore);
                PlayerPrefs.SetFloat(currentTimeKey, timeLeft); // Add this line
                return;
            }              
        }
    }

    public void DisplayHighScores()
    {
        for (int i = 0; i < NUM_HIGH_SCORES; i++)
        {
            nameTexts[i].text = PlayerPrefs.GetString(NAME_KEY + (i+1));
            if(PlayerPrefs.GetInt(SCORE_KEY + (i+1)) != 0) {
                scoreTexts[i].text = PlayerPrefs.GetInt(SCORE_KEY + (i+1)).ToString();
            }
            if(PlayerPrefs.GetFloat(TIME_KEY +(i+1)) != 0) {
                float rawTime = PlayerPrefs.GetFloat(TIME_KEY + (i+1));
                int hours = Mathf.FloorToInt(rawTime / 3600F);
                int minutes = Mathf.FloorToInt((rawTime % 3600) / 60F);
                timeTexts[i].text = string.Format("{0:00}:{1:00}", hours, minutes); // Display the time in "00:00" format
            }
            else {
                timeTexts[i].text = "0:00";
            }
        }
    }


}
