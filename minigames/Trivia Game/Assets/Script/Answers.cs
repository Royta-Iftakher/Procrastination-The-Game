using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answers : MonoBehaviour
{
    public bool isCorrect = false; 
    public QuizManager manager;

    public void Answer(){
        if (isCorrect){
            Debug.Log("Correct Answer!");
            manager.correct();
        }else{
            Debug.Log("Wrong Answer!");
            manager.incorrect();
        }
    }  
}
