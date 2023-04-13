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
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        if(hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            AudioSource.PlayClipAtPoint(audio.clip, transform.position);
            SetTimerText();
            timerText.color = Color.red;
            enabled = false;
            endScreen();
        }

        if (currentTime <= 10 ) {
            timerText.color = Color.Lerp(timerText.color, orangeColor, Time.deltaTime);
        }

        SetTimerText();
    }

    private void SetTimerText() 
    {
        timerText.text = currentTime.ToString("00.##");
    }

    private void endScreen() 
    {
        TypingScreen.SetActive(false);
        endingScreen.SetActive(true);
    }
}
