using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public string sceneName;                                //sceneName is currently TutorialPage1       
    public void PlayGame()
    {
        AudioManager.instance.DisableAudioSource("mainTheme");
        GameTimer.Instance.EnableChildren();
        GameTimer.Instance.ResetTimer();
        GameManager.Instance.gameStarted = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    public void OnMenuButtonClick()
    {
        //Debug.Log("Button clicked!");
        AudioManager.instance.PlayOpenBook();
        GameManager.Instance.sceneFinisher();                                      //sceneFinisher is a method in GameManager: it sets all the objects in the menu active
        SceneManager.UnloadSceneAsync(sceneName);                     //unloads the tutorial scene when the menu button is clicked in tutorial
    }

    public void OnTutorialButtonClick()
    {
        //Debug.Log("Button clicked!");
        AudioManager.instance.PlayDefaultButton();
        GameManager.Instance.sceneLoader();                                     //sceneLoader is a method in GameManager: it sets all the objects in the menu unactive
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);  //loads the tutorial scene when the tutorial button is clicked
        //SceneManager.LoadScene("TutorialPage1");
    }
}

