using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public GameObject eventSystemPrefab;

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
            if (eventSystemPrefab != null)
            {
                Instantiate(eventSystemPrefab);
            }
            else
            {
                Debug.LogError("EventSystem prefab is null.");
            }
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
            if (eventSystemPrefab != null)
            {
                Instantiate(eventSystemPrefab);
            }
            else
            {
                Debug.LogError("EventSystem prefab is null.");
            }
        }
    }
}
