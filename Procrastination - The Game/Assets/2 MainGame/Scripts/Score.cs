using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    private Dictionary<string, int> activityPoints = new Dictionary<string, int>();
    public Dictionary<string, int> ActivityScores { get; private set; } = new Dictionary<string, int>();
    public Dictionary<string, bool> ActivityDone { get; private set; } = new Dictionary<string, bool>();  // track whether each activity is done
    public int letterGrade = 5;

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

        letterGrade = 5;
        // Initialize activities and their points
        activityPoints.Add("trash", 15);
        activityPoints.Add("email", 20);
        activityPoints.Add("read", 15);
        activityPoints.Add("laundry", 10);
        activityPoints.Add("food", 10);
        activityPoints.Add("socialize", 10);
        activityPoints.Add("games", 10);
        activityPoints.Add("exercise", 10);

        // Initialize the scores and done statuses
        foreach(string activity in activityPoints.Keys)
        {
            ActivityScores.Add(activity, 0);
            ActivityDone.Add(activity, false);  // initially, no activity is done
        }
    }

    public void AddScore(string activity)
    {
        if (activityPoints.ContainsKey(activity))
        {
            ActivityScores[activity] += activityPoints[activity];
            DataManager.Instance.SetScore(ActivityScores[activity]);
            MarkActivityDone(activity);

            // Update letter grade
            int totalScore = 0;
            foreach(var score in ActivityScores.Values)
            {
                totalScore += score;
            }

            if (totalScore >= 100)
                letterGrade = 0;
            else if (totalScore >= 90)
                letterGrade = 1;
            else if (totalScore >= 80)
                letterGrade = 2;
            else if (totalScore >= 70)
                letterGrade = 3;
            else if (totalScore >= 60)
                letterGrade = 4;
            else
                letterGrade = 5;
        }
        else
        {
            Debug.LogError("Activity not found in points list");
        }
    }

    public void MarkActivityDone(string activity)
    {
        if (ActivityDone.ContainsKey(activity))
        {
            ActivityDone[activity] = true;
        }
        else
        {
            Debug.LogError("Activity not found in ActivityDone");
        }
    }

    public void ResetScores()
    {
        foreach (var activity in ActivityScores.Keys.ToList())
        {
            ActivityScores[activity] = 0;
        }

        foreach (var activity in ActivityDone.Keys.ToList())
        {
            ActivityDone[activity] = false;
        }

        letterGrade = 5; // reset to the lowest grade

        DataManager.Instance.SetScore(0); // reset DataManager's score
    }

}
