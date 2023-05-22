using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeControl : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI percentage;
    
    public AudioMixer audioMixer;
    private float newVolume;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0);
            Load();
        }
        else
        {
            Load();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        float clampedVolume = Mathf.Clamp(volume, -80f, 0f);
        float newVolume = (clampedVolume + 80f) / 80f * 100f;
        percentage.text = newVolume < 10 ? newVolume.ToString("0") + "%" : newVolume.ToString("00") + "%";
        Save();
    }

    private void Load()
    {
        float volume = PlayerPrefs.GetFloat("volume");
        slider.value = volume;

        // Set the audio mixer's volume
        audioMixer.SetFloat("volume", volume);

        bool result = audioMixer.GetFloat("volume", out volume);
        // Calculate the volume percentage and update the UI
        if (result)
        {
            float clampedVolume = Mathf.Clamp(volume, -80f, 0f);
            newVolume = (clampedVolume + 80f) / 80f * 100f;
            percentage.text = newVolume < 10 ? newVolume.ToString("0") + "%" : newVolume.ToString("00") + "%";
        }

    }

    
    private void Save()
    {
        PlayerPrefs.SetFloat("volume", slider.value);
    }
}