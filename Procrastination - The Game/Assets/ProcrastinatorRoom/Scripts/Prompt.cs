using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prompt : MonoBehaviour
{
    public GameObject promptCanvas;
    public string sceneName;
    public GameObject player;
    void Start()
    {
        hidePrompt();
    }
    public void goToScene() {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            obj.SetActive(false);
        }

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
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
}
