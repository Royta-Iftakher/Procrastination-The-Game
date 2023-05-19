using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip openBook;
    public AudioClip defaultButton;
    public AudioSource mainTheme; 

    private AudioSource[] audioSources;

    private void Awake()
    {
        // Make sure only one instance of AudioManager is created
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Get the audio source component
        audioSources = GetComponents<AudioSource>();
    }

    public void DisableAudioSource(string clipName)
    {
        // Find the AudioSource with the given clip name
        foreach (AudioSource source in audioSources)
        {
            if (source.clip.name == clipName)
            {
                // Disable the AudioSource if it exists
                source.enabled = false;
                break;
            }
        }
    }
    public void EnableAudioSource(string clipName)
    {
        // Find the AudioSource with the given clip name
        foreach (AudioSource source in audioSources)
        {
            if (source.clip.name == clipName)
            {
                // Enable the AudioSource if it exists
                source.enabled = true;
                break;
            }
        }
    }



    public void PlaySound(AudioClip clip)
    {
        audioSources[0].clip = clip;
        audioSources[0].Play();
    }

    public void PlayOpenBook()
    {
        PlaySound(openBook);
    }

    public void PlayDefaultButton()
    {
        PlaySound(defaultButton);
    }

}
