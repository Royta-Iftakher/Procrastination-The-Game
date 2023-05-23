using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{

    void Start()
    {
        
    }

    public void goBack() {
        
        PauseMenu.Instance.readTask(true);
        GameManager.Instance.gameFinished = true;
        SceneManager.UnloadSceneAsync(GameManager.Instance.sceneName);
    }
}
