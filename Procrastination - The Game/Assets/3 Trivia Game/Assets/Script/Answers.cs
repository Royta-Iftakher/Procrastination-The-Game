using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answers : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager manager;
    public Image buttonImage;

    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("Correct Answer!");
            manager.correct();
            StartCoroutine(FlashButton(Color.green));
        }
        else
        {
            Debug.Log("Wrong Answer!");
            manager.incorrect();
            StartCoroutine(FlashButton(Color.red));
        }
    }

    private IEnumerator FlashButton(Color color)
    {
        buttonImage.color = color;

        yield return new WaitForSeconds(0.5f);

        buttonImage.color = Color.white;
    }
}
