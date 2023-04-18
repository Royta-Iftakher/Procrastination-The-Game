using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    
    public string sceneName;
    public GameObject eventSystemPrefab;
    public bool spawned = false;
    public bool gameFinished = false;
    private GameObject[] allObjects;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (!GameObject.Find("EventSystem"))
        {
            Instantiate(eventSystemPrefab);
        }
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
        foreach (GameObject obj in allObjects) {
            if (obj.name != "GameManager") { 
                obj.SetActive(false);
            }
        }
    }

    public void sceneFinisher()
    {
        foreach(GameObject obj in allObjects) {
            obj.SetActive(true);
        }
    }

    void Update()
    {
        if(gameFinished == true) {
            gameFinished = false;
            sceneFinisher();
        }
    }
    
}
