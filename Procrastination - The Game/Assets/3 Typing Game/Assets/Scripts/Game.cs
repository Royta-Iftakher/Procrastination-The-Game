using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject GameScreen;
    public GameObject EndingScreen; 
    public GameObject StartingScreen;
    public GameObject WinningScreen;

    private Typer typer;
    private void Awake() {
        GameScreen.SetActive(false);   // Disable the Game Over game object in the UI
        EndingScreen.SetActive(false); // Disable the Game Win game object in the UI
        WinningScreen.SetActive(false);
        Pause();                                           
    }
    // Start is called before the first frame update
    void Start()
    {
        typer = FindObjectOfType<Typer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(typer.gameWon) {
            GameScreen.SetActive(false);
            WinningScreen.SetActive(true);
        }
    }

    public void Pause()  // method for pausing the game
    {
        Time.timeScale = 0f; // stop time
    }

    public void Play() // method for starting the game
    {   
        StartingScreen.SetActive(false);
        GameScreen.SetActive(true);   // Enable the Game Over game object in the UI
        Time.timeScale = 1f; // Unpause time
    }

    public void goBack() {
        SceneManager.UnloadSceneAsync("TypingGame");
        GameManager.Instance.sceneFinisher();
        Time.timeScale = 1f;                        //so does it doesn't pause main game after pressing go back
    }

    public void goBackFinished() {
        PauseMenu.Instance.emailsTask(true);
        SceneManager.UnloadSceneAsync("TypingGame");
        GameManager.Instance.sceneFinisher();
        Time.timeScale = 1f;                 
          
    }
}
