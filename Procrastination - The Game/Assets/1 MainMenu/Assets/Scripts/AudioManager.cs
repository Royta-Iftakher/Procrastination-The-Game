using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip openBook;
    public AudioClip defaultButton;
    public AudioClip friendTalking;
    public AudioClip point;
    public AudioClip buttonClick;
    public AudioClip kitchenClick;
    public AudioClip phoneRinging; // New audio clip for phone ringing
    public AudioSource MainGameMusic;
    public AudioSource mainTheme;

    private AudioSource[] audioSources;
    [SerializeField] private AudioMixer audioMixer;

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

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat("volume");

        // Set the audio mixer's volume
        audioMixer.SetFloat("volume", volume);
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

    public void PhoneCall()
    {
        PlaySound(friendTalking);
    }

    public void Point()
    {
        PlaySound(point);
    }

    public void ButtonClick()
    {
        PlaySound(buttonClick);
    }

    public void KitchenButtonClick()
    {
        PlaySound(kitchenClick);
    }

    public void PhoneRinging()
    {
        PlaySound(phoneRinging); // Play the phone ringing sound
    }

    public void StopPhoneRinging()
    {
        StopSound(phoneRinging); // Stop the phone ringing sound
    }

    public float GetCurrentClipLength()
    {
        // Return the length of the friendTalking clip
        if (friendTalking != null)
        {
            return friendTalking.length;
        }

        // Return 0 if there is no audio clip
        return 0;
    }

    public void PauseAllAudio()
    {
        // Pause all active AudioSources
        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying)
            {
                source.Pause();
            }
        }
    }

    public void ResumeAllAudio()
    {
        // Resume all active AudioSources
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                source.UnPause();
            }
        }
    }

    public void StopSound(AudioClip clip)
    {
        // Find the AudioSource playing the given clip and stop it
        foreach (AudioSource source in audioSources)
        {
            if (source.clip == clip && source.isPlaying)
            {
                source.Stop();
                break;
            }
        }
    }
}
