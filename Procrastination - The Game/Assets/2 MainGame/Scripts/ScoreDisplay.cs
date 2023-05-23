using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class ActivityDisplayMapping
{
    public string ActivityName;
    public TextMeshProUGUI ActivityText;
    public TextMeshProUGUI PointsText;
}

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private List<ActivityDisplayMapping> activityMappings;
    [SerializeField] private TextMeshProUGUI totalScoreText; // Reference to the text object where the total score will be displayed
    [SerializeField] private TextMeshProUGUI totalPointsText;

    public float typingSpeed = 10f; // Speed at which the scores are "typed out"

    void Start()
    {
        StartCoroutine(DisplayScoresCoroutine());
    }

    IEnumerator DisplayScoresCoroutine()
    {
        int totalScore = 0; // Initialize total score

        foreach (var mapping in activityMappings)
        {
            if (Score.Instance.ActivityScores.ContainsKey(mapping.ActivityName))
            {
                int activityScore = Score.Instance.ActivityScores[mapping.ActivityName];
                mapping.PointsText.text = $"{activityScore}";

                totalScore += activityScore; // Add activity score to total score

                if (Score.Instance.ActivityDone.ContainsKey(mapping.ActivityName) && Score.Instance.ActivityDone[mapping.ActivityName])
                {
                    StartCoroutine(TypeOutTextCoroutine(mapping.ActivityText, $"-{mapping.ActivityName} : Done"));
                }
                else
                {
                    StartCoroutine(TypeOutTextCoroutine(mapping.ActivityText, $"-{mapping.ActivityName} : Not Done"));
                }
            }
            else
            {
                Debug.LogError($"Activity not found in activityScores: {mapping.ActivityName}");
            }
        }

        StartCoroutine(TypeOutTextCoroutine(totalScoreText, $"Total Score: "));
        StartCoroutine(TypeOutTextCoroutine(totalPointsText, $"{totalScore}"));

        yield return null;
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
