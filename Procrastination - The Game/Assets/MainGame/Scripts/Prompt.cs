using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prompt : MonoBehaviour
{
    public GameObject promptCanvas;
    public GameObject player;
    public GameManager manager;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        hidePrompt();
    }
    public void goToScene() {
        hidePrompt();
        manager.sceneLoader();
        SceneManager.LoadScene(manager.sceneName, LoadSceneMode.Additive);
    }
    public void cancel() {
        hidePrompt();
    }
    public void hidePrompt() {
        promptCanvas.SetActive(false);
    }
    public void hidePlayer() {
        player.SetActive(false);
    }

    void Update() {
        
    }
}
