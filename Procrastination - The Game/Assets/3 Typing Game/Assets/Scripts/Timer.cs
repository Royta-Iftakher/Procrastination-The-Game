using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] public GameObject endingScreen;
    [SerializeField] public GameObject TypingScreen;
    public TextMeshProUGUI timerText;
    [SerializeField] AudioSource audio;

    public float currentTime;
    public bool countDown = true;

    public bool hasLimit;
    public float timerLimit;

    public Color orangeColor;
    
    private float updateInterval = 0.1f; // Update every second
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
          if (audio == null)
            audio = GetComponent<AudioSource>();
         
        orangeColor = new Color(1.0f, 0.5f, 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        if(hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            AudioSource.PlayClipAtPoint(audio.clip, transform.position);
            timerText.color = Color.red;
            enabled = false;
            endScreen();
        }

        if (currentTime <= 10 ) {
            timerText.color = Color.Lerp(timerText.color, orangeColor, Time.deltaTime);
        }

        if (timer > updateInterval)
        {
            timer = 0f;
            SetTimerText();
        }
    }


    private void SetTimerText() 
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }



    private void endScreen() 
    {
        TypingScreen.SetActive(false);
        endingScreen.SetActive(true);
        PauseMenu.Instance.emailsFail();
    }
}
