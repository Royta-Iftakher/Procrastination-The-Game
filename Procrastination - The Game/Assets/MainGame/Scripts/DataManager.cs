using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private float playerTimeleft;
    [SerializeField] private string playerName;
    [SerializeField] public List<HighScore> highScores;

    public static DataManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTimeleft = 0;
        playerName = "";
        highScores = new List<HighScore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName(string s)
    {
        playerName = s;
    }

    public void SetScore(float s)
    {
        playerTimeleft = s;
    }

    public string GetName()
    {
        return playerName;
    }

    public float GetScore()
    {
        return playerTimeleft;
    }

    public void AddHighScore(string name, float timeLeft)
    {
        highScores.Add(new HighScore(name, timeLeft));
        highScores.Sort();
        if (highScores.Count > 5)
        {
            highScores.RemoveAt(5);
        }
    }

    public List<HighScore> GetHighScores()
    {
        return highScores;
    }

    public struct HighScore : System.IComparable<HighScore>
    {
        public string name;
        public float timeLeft;

        public HighScore(string name, float timeLeft)
        {
            this.name = name;
            this.timeLeft = timeLeft;
        }

        public int CompareTo(HighScore other)
        {
            return other.timeLeft.CompareTo(this.timeLeft);
        }
    }
}
