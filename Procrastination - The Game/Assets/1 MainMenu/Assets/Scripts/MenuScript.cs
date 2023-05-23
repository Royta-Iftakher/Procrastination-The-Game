using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public string sceneName;                                    
    public void PlayGame()
    {
        AudioManager.instance.DisableAudioSource("mainTheme");
        AudioManager.instance.EnableAudioSource("MainGameMusic");
        GameManager.Instance.outside = true;
        GameManager.Instance.inside = false;
        GameTimer.Instance.EnableChildren();
        GameTimer.Instance.ResetTimer();
        Score.Instance.ResetScores();
        GameManager.Instance.gameStarted = true;
        int selectedWatchIndex = GameTimer.Instance.GetSelectedWatch();
        Watch watch = FindObjectOfType<Watch>();  // Finds the first Watch object in the scene - adjust if necessary
        watch.SetWatch(selectedWatchIndex);
        SceneManager.LoadScene("Outside");
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
        sceneName = "TutorialPage1";
        AudioManager.instance.PlayDefaultButton();
        GameManager.Instance.sceneLoader();                                     //sceneLoader is a method in GameManager: it sets all the objects in the menu unactive
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);  //loads the tutorial scene when the tutorial button is clicked
        //SceneManager.LoadScene("TutorialPage1");
    }

    public void OnOptionsButtonClick()
    {
        //Debug.Log("Button clicked!");
        sceneName = "OptionsScene";
        AudioManager.instance.PlayDefaultButton();
        GameManager.Instance.sceneLoader();                                     //sceneLoader is a method in GameManager: it sets all the objects in the menu unactive
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);  //loads the tutorial scene when the tutorial button is clicked
        //SceneManager.LoadScene("TutorialPage1");
    }

    public void OnLeaderboardButtonClick()
    {
        sceneName = "HighScoreScene";
        AudioManager.instance.PlayDefaultButton();
        GameManager.Instance.sceneLoader();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void OnStartButtonClick() {
        sceneName = "Customization";
        DataManager.Instance.ResetData();
        GameTimer.Instance.ResetData();
        AudioManager.instance.PlayOpenBook();
        GameManager.Instance.sceneLoader();
        SceneManager.LoadScene(sceneName);
    }

    public void OnCustomizationMenuClick() {
        AudioManager.instance.PlayOpenBook();
        SceneManager.LoadScene("Menu");
    }

}

