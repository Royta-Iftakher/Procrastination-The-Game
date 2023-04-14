using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    private GameManager manager;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        
    }

    public void goBack() {
        SceneManager.UnloadSceneAsync("TypingGame");
        manager.sceneFinisher();
    }
}
