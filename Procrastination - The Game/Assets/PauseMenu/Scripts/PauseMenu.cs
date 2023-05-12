using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }

    public static bool GameIsPaused = false;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private PlayerMovement player;
    // Update is called once per frame
    public bool trashDone;
    public bool readDone;
    public bool emailsDone;

    [SerializeField] private Toggle trashToggle;
    [SerializeField] private Toggle readToggle;
    [SerializeField] private Toggle emailsToggle;

    private void Awake()
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
    }

    void Start() {
        pauseMenuUI.SetActive(false);
        player = FindObjectOfType<PlayerMovement>();
        trashDone = false;
        readDone = false;
        emailsDone = false;

        trashToggle.onValueChanged.AddListener(trashTask);
        readToggle.onValueChanged.AddListener(readTask);
        emailsToggle.onValueChanged.AddListener(emailsTask);

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else 
            {
                Pause();
            }
        }
        if(trashDone && readDone && emailsDone) {
            GameManager.Instance.win = true;
            Resume();
        }

        trashToggle.isOn = trashDone;
        readToggle.isOn = readDone;
        emailsToggle.isOn = emailsDone;
    }

    public void Resume()
    {
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        
    }

    public void Pause()
    {   
        GameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        
    }

    public void Quit()
    {
        Resume();
        GameTimer.Instance.gameObject.SetActive(false);
        GameManager.Instance.gameStarted = false;
        SceneManager.LoadScene("Menu");
        AudioManager.instance.EnableAudioSource("mainTheme");
         GameManager.Instance.spawned = false;
        
    }

    public void trashTask(bool isComplete)
    {
        trashDone = isComplete;
        Debug.Log("Trash completed");
        
    }

    public void readTask(bool isComplete)
    {
        readDone = isComplete;
    }

    public void emailsTask(bool isComplete) 
    {
        emailsDone = isComplete;
    }
}
