using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    
    public string sceneName;
    [SerializeField] private GameObject eventSystemPrefab;
    public bool spawned = false;
    public bool gameFinished = false;
    private GameObject[] allObjects;
    public bool gameStarted = false;
    public bool win;
    public bool inTask;
    private PlayerMovement player;
    private Energy energy;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        Application.targetFrameRate = 60; 
    }

    void Start()
    {
        if (!GameObject.Find("EventSystem"))
        {
            Instantiate(eventSystemPrefab);
        }
        win = false;
        player = FindObjectOfType<PlayerMovement>();
        energy = FindObjectOfType<Energy>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!GameObject.Find("EventSystem"))
        {
            Instantiate(eventSystemPrefab);
        }

        // spawn the player at the designated spawn point

    }

    public void sceneLoader() 
    {
        allObjects = GameObject.FindObjectsOfType<GameObject>();
        player.DisablePlayerControls();
        GameTimer.Instance.DisableChildren();
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("GameManager") || obj.CompareTag("Player"))
            {
                continue; // Skip deactivating GameManager and Player objects
            }   
            obj.SetActive(false);
        }
    }

    public void sceneFinisher()
    {
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("GameManager") || obj.CompareTag("Player"))
            {
                continue; // Skip deactivating GameManager and Player objects
            }   
            obj.SetActive(true);
        }
        player.EnablePlayerControls();
        GameTimer.Instance.EnableChildren();
    }

    void Update()
    {
        if(gameFinished == true) {
            gameFinished = false;
            sceneFinisher();
        }
        if(win == true) {
            SceneManager.LoadScene("End");
        }
        if(player == null) {
            player = FindObjectOfType<PlayerMovement>();
        }
    }
    
}
