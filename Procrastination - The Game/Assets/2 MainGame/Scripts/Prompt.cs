using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prompt : MonoBehaviour
{
    public GameObject promptCanvas;

    void Start()
    {
        hidePrompt();
    }
    public void goToScene() {
        hidePrompt();
        GameManager.Instance.inTask = true;
        GameManager.Instance.sceneLoader();
        SceneManager.LoadScene(GameManager.Instance.sceneName, LoadSceneMode.Additive);
    }
    public void cancel() {
        hidePrompt();
    }
    public void hidePrompt() {
        promptCanvas.SetActive(false);
    }


    void Update() {
        
    }
}
