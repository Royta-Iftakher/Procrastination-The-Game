using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    
    public void menuButton() {
        AudioManager.instance.PlayOpenBook();
        SceneManager.LoadScene("Menu");
        GameManager.Instance.ResetGame();
    }
}
