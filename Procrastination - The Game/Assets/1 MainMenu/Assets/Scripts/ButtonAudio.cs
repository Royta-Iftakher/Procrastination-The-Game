using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    public AudioSource defaultButton;

    public AudioSource openBook;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDefaultSound()
    {
        defaultButton.Play();
    }

    public void PlayOpenBookSound()
    {
        openBook.Play();
    }
}
