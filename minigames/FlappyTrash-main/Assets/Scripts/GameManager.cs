using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Player player;
    private Spawner spawner;

    public Text scoreText;
    public Text instructions;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject gameWin;
    public bool finishedGame = false;
    public bool lose = false;
    public int score { get; private set; }
    private void Awake()
    {
        instructions.enabled = true;
        Application.targetFrameRate = 60;
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        gameOver.SetActive(false);
        gameWin.SetActive(false);

        Pause();
    }

    public void Play()
    {
        if(finishedGame == true) {
            RestartGame();
            finishedGame = false;
        }
        Invoke("HideInstructions", 2f);
        lose = false;
        score = 0;
        scoreText.text = score.ToString();
        playButton.SetActive(false);
        gameOver.SetActive(false);
        gameWin.SetActive(false);
        

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++) {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        lose = true;
        playButton.SetActive(true);
        gameOver.SetActive(true);
        ShowScoreText();
        Pause();
    }
    public void GameWin()
    {
        finishedGame = true;
        playButton.SetActive(true);
        gameWin.SetActive(true);
        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        if(score == 10 && lose == false) {
            Invoke("HideScoreText", .5f);
        }
    }

    void HideScoreText() {
        scoreText.enabled = false;
    }

    void ShowScoreText() {
        scoreText.enabled = true;
    }
    void HideInstructions() {
        instructions.enabled = false;
    }
    public void RestartGame()
    {
    // Reload the current scene
    SceneManager.LoadScene(0);
    }

}
