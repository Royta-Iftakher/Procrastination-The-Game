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
        
        manager.gameFinished = true;
        SceneManager.UnloadSceneAsync("TypingGame");
    }
}
