using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }

    public static bool GameIsPaused = false;

    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Energy energy;
    [SerializeField] private LaundryBasket laundryBasket;
    [SerializeField] private Score score;
    // Update is called once per frame
    public bool trashDone;
    public bool readDone;
    public bool emailsDone;
    public bool laundryDone;
    public bool foodCooked;

    [SerializeField] private Toggle trashToggle;
    [SerializeField] private Toggle readToggle;
    [SerializeField] private Toggle emailsToggle;
    [SerializeField] private Toggle laundryToggle;
    



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
        energy = FindObjectOfType<Energy>();
        score = FindObjectOfType<Score>();

        trashDone = false;
        readDone = false;
        emailsDone = false;
        laundryDone = false;
        foodCooked = false;

        laundryToggle.onValueChanged.AddListener(laundryTask);
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
        if(trashDone && emailsDone && laundryDone) {
            GameManager.Instance.win = true;
            Resume();
        }
        if(laundryBasket == null ) {
            laundryBasket = FindObjectOfType<LaundryBasket>();
        }
    }

    public void Quit()
    {
        if(GameManager.Instance.inTask) {
            GameManager.Instance.sceneFinisher();
            GameManager.Instance.inTask = false;
        }
        Resume();
        AudioManager.instance.DisableAudioSource("MainGameMusic");
        AudioManager.instance.PlayOpenBook();
        SceneManager.LoadScene("Menu");
        GameTimer.Instance.timerReset();
        Score.Instance.ResetScores();
        GameManager.Instance.ResetGame();
        Destroy(Instance.gameObject);
        
    }

    public void trashTask(bool isComplete)
    {
        GameManager.Instance.inTask = false;
        trashDone = isComplete;
        Debug.Log("Trash completed");
        energy.LoseEnergy(2);
        score.AddScore("trash");
    }

    public void readTask(bool isComplete)
    {
        GameManager.Instance.inTask = false;
        readDone = isComplete;
        energy.LoseEnergy(2);
        score.AddScore("read");
    }

    public void emailsTask(bool isComplete) 
    {
        GameManager.Instance.inTask = false;
        emailsDone = isComplete;
        Debug.Log("Emails completed");
        energy.LoseEnergy(2);
        score.AddScore("email");
    }

    public void laundryTask(bool isComplete)
    {
        laundryDone = isComplete;
        Debug.Log("Laundry completed");
        laundryBasket.EmptyBasket();
        energy.LoseEnergy(1);
        score.AddScore("laundry");
    }

    public void foodSim(bool isComplete) {
        GameManager.Instance.inTask = false;
        foodCooked = isComplete;
        energy.GainEnergy(2);
        score.AddScore("food");
    }

    public void UpdateToggleUI()
    {
        trashToggle.SetIsOnWithoutNotify(trashDone);
        readToggle.SetIsOnWithoutNotify(readDone);
        emailsToggle.SetIsOnWithoutNotify(emailsDone);
        laundryToggle.SetIsOnWithoutNotify(laundryDone);
    }

    public void Resume()
    {
        pauseButton.gameObject.SetActive(true);
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        UpdateToggleUI();
        
    }

    public void Pause()
    {   
        pauseButton.gameObject.SetActive(false);
        GameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        UpdateToggleUI();
    }

    public void emailsFail() 
    {
        energy.LoseEnergy(2);
    }

    public void Restart()
    {
        Destroy(Instance.gameObject);
    }
}
