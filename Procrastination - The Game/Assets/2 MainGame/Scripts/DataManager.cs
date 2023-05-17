using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private string playerTimeleft;
    [SerializeField] private string playerName;
    [SerializeField] private int playerScore;

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
        playerTimeleft = "";
        playerName = "";
        playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName(string s)
    {
        playerName = s;
    }

    public void SetTime(string s)
    {
        playerTimeleft = s;
    }

    public void SetScore(int score)
    {
        playerScore = score;
    }

    public string GetName()
    {
        return playerName;
    }

    public string GetTime()
    {
        return playerTimeleft;
    }

    public int GetScore()
    {
        return playerScore;
    }

}
