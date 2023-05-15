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
    [SerializeField] private Energy energy;
    [SerializeField] private LaundryBasket laundryBasket;
    // Update is called once per frame
    public bool trashDone;
    public bool readDone;
    public bool emailsDone;
    public bool laundryDone;

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
        trashDone = false;
        readDone = false;
        emailsDone = false;
        laundryDone = false;

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
        if(trashDone && readDone && emailsDone && laundryDone) {
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
        energy.currentEnergy = 5;
        
        Resume();
        GameTimer.Instance.DisableChildren();
        GameManager.Instance.gameStarted = false;
        AudioManager.instance.EnableAudioSource("mainTheme");
        GameManager.Instance.spawned = false;
        Destroy(Instance.gameObject);
        SceneManager.LoadScene("Menu");
    }

    public void trashTask(bool isComplete)
    {
        trashDone = isComplete;
        Debug.Log("Trash completed");
        energy.LoseEnergy(2);
    }

    public void readTask(bool isComplete)
    {
        readDone = isComplete;
    }

    public void emailsTask(bool isComplete) 
    {
        emailsDone = isComplete;
        Debug.Log("Emails completed");
        energy.LoseEnergy(2);
    }

    public void laundryTask(bool isComplete)
    {
        laundryDone = isComplete;
        Debug.Log("Laundry completed");
        laundryBasket.EmptyBasket();
        energy.LoseEnergy(1);
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
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        UpdateToggleUI();
    }

    public void Pause()
    {   
        GameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        UpdateToggleUI();
}

}
