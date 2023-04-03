using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typer : MonoBehaviour
{
    public WordBank wordBank = null;
    public TextMeshProUGUI wordOutput = null;

    [SerializeField] private string remainingWord = string.Empty;
    [SerializeField] private string currentWord = string.Empty;
    
    // Start is called before the first frame update
    private void Start()
    {
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        currentWord = wordBank.GetWord();
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString) 
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
    }
    // Update is called once per frame
    private void Update()
    {
        Checkinput();
    }

    private void Checkinput() 
    {
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            if (keysPressed.Length == 1) 
            {
                EnterLetter(keysPressed);
            }
        }
    }

    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter)) 
        {
            RemoveLetter();

            if (IsWordComplete()) 
            {
                SetCurrentWord();
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }
}
