using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public PlayerMovement player;
    // Update is called once per frame


    void Start() {
        pauseMenuUI.SetActive(false);
        player = FindObjectOfType<PlayerMovement>();

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else 
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        
    }

    public void Pause()
    {   
        GameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        
    }

    public void Quit()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        GameTimer.Instance.gameObject.SetActive(false);
        SceneManager.LoadScene("Menu");
        AudioManager.instance.EnableAudioSource("mainTheme");
    }
}
