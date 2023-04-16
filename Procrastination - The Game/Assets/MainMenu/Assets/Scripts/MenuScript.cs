using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private GameManager manager;                             // Allows Manager to be in all scenes and helps connects the whole game
    public string sceneName;                                //sceneName is currently TutorialPage1       
    void Start() {
        manager = FindObjectOfType<GameManager>();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnMenuButtonClick()
    {
        //Debug.Log("Button clicked!");
        manager.sceneFinisher();                                      //sceneFinisher is a method in GameManager: it sets all the objects in the menu active
        SceneManager.UnloadSceneAsync(sceneName);                     //unloads the tutorial scene when the menu button is clicked in tutorial
    }

    public void OnTutorialButtonClick()
    {
        //Debug.Log("Button clicked!");
        manager.sceneLoader();                                      //sceneLoader is a method in GameManager: it sets all the objects in the menu unactive
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);  //loads the tutorial scene when the tutorial button is clicked
        //SceneManager.LoadScene("TutorialPage1");
    }
}

//i hope this way will make it easier for the music to stop cutting off
//if u are confused ask questions
//also scene additive would help with the ui stuff later for example, if when we create the pause menu

