using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Player player;                          // Reference to Player script component
    private Spawner spawner;                        // Reference to Spawner script component
    private GameManager manager;                    // Reference to GameManager script component

    public Text scoreText;                          // Reference to the score text component in the UI
    public Text instructions;                       // Reference to the instructions text component in the UI
    public GameObject playButton;                   // Reference to the Play button game object in the UI
    public GameObject gameOver;                     // Reference to the Game Over game object in the UI
    public GameObject gameWin;                      // Reference to the Game Win game object in the UI

    public bool finishedGame = false;                // Boolean to check if the game is finished
    public bool lose = false;                        // Boolean to check if the player has lost
    public int score { get; private set; }           // The current score of the player (can only be set within the GameController)
    private int fails = 0;                           // The number of times the player has failed to reach the truck

    private void Awake()
    {
        instructions.enabled = true;                        // Enable the instructions at the start of the game
        Application.targetFrameRate = 60;                    // Set the target frame rate to 60 fps
        player = FindObjectOfType<Player>();                // Find the Player script component
        spawner = FindObjectOfType<Spawner>();              // Find the Spawner script component
        manager = FindObjectOfType<GameManager>();          // Find the GameManager script component
        gameOver.SetActive(false);                          // Disable the Game Over game object in the UI
        gameWin.SetActive(false);                           // Disable the Game Win game object in the UI

        Pause();                                            // Pause the game at the start
    }

    public void Play()
    {
        if(finishedGame == true) {                          // If the game is finished (score is 10 and the player reaches the truck)
            EndGame();                                      // Call the EndGame method and go back to the main game
            finishedGame = false;                           // Set finishedGame to false
        }
        Invoke("HideInstructions", 2f);                     // Hide instructions few seconds after hitting play
        lose = false;                                       // Reset lose to false
        score = 0;                                          // Reset the score to zero
        scoreText.text = score.ToString();                  // Update the score text component in the UI
        playButton.SetActive(false);                        // Hide the UI when the game starts
        gameOver.SetActive(false);                          // Disable the Game Over game object in the UI
        gameWin.SetActive(false);                           // Disable the Game Win game object in the UI
        
        Time.timeScale = 1f;                                // Set the time scale to 1 (normal speed)
        player.enabled = true;                              // Enable the Player script component

        Pipes[] pipes = FindObjectsOfType<Pipes>();         // Find all the Pipes prefabs

        for (int i = 0; i < pipes.Length; i++) {            // Destroy the Pipes prefabs that are away from the screen
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
{
    lose = true;                                                // set the lose flag to true
    playButton.SetActive(true);                                // set the playButton active
    gameOver.SetActive(true);                                   // set the gameOver UI active
    ShowScoreText();                                            // show the score text
    Pause();                                                    // pause the game
}

public void GameWin()
{
    finishedGame = true;                                        // set the finishedGame flag to true
    playButton.SetActive(true);                                // set the playButton active
    gameWin.SetActive(true);                                    // set the gameWin UI active
    Pause();                                                    // pause the game
}

public void Pause()                                             // method for pausing the game
{
    Time.timeScale = 0f;                                        // stop time
    player.enabled = false;                                     // disable player movement
}

public void IncreaseScore()                                     // method for increasing the score
{
    score++;                                                    // increase the score by 1
    scoreText.text = score.ToString();                          // update the score text on screen
    if(score == 10 && lose == false) {                           // if the score is 10 and the lose flag is false
        Invoke("HideScoreText", .5f);                            // invoke the HideScoreText method after 0.5 seconds
    }
}

void HideScoreText() {                                          // method for hiding the score text
    scoreText.enabled = false;                                  // disable the score text
}

void ShowScoreText() {                                          // method for showing the score text
    scoreText.enabled = true;                                   // enable the score text
}

void HideInstructions() {                                       // method for hiding the instructions
    instructions.enabled = false;                               // disable the instructions
}

public void EndGame()                                           // method for ending the game
{
    manager.gameFinished = true;                                // set the gameFinished flag in the GameManager
    SceneManager.UnloadSceneAsync("FlappyTrash");               // unload the current scene
}

}
//Find a way to -.5 for bottom pipe and +.5 for top pipe everytime player loses 5 times