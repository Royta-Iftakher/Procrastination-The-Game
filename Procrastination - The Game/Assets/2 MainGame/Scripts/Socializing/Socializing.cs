using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Socializing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.EnableAudioSource("restaurant");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void socialize() {
        AudioManager.instance.DisableAudioSource("restaurant");
        PauseMenu.Instance.socialTask(true);
        GameManager.Instance.sceneFinisher();
        SceneManager.UnloadSceneAsync("Socialize");
        GameTimer.Instance.FastForwardTime();
    }
}
