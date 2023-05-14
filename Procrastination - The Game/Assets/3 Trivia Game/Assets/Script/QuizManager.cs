using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class QuizManager : MonoBehaviour
{
    public List<QandA> QuesAnswer;
    public GameObject[] options;
    public int currentQuestion;
    public GameObject Quizpanel;
    public GameObject GameoverPanel;
    public TextMeshProUGUI QuestionTxt;
    public TextMeshProUGUI scoreTxt;
    int totalquestions = 0;
    public static int score = 0;

    private void Start(){
        QuesAnswer = QuesAnswer.OrderBy(x => Random.value).ToList(); 
        totalquestions = Mathf.Min(QuesAnswer.Count, 10);
        GameoverPanel.SetActive(false);
        generateQuestion();     
    }

    public void correct(){
        score = score + 1;
        QuesAnswer.RemoveAt(currentQuestion);
        generateQuestion();
    }

    public void incorrect(){
        QuesAnswer.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void setAnswers(){
        for(int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Answers>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QuesAnswer[currentQuestion].Answers[i];

            if(QuesAnswer[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<Answers>().isCorrect = true;
            }
        }
    }

    void generateQuestion(){
        if(currentQuestion < totalquestions){
            QuestionTxt.text = QuesAnswer[currentQuestion].Question;
            setAnswers();
            currentQuestion++;
        }
        else{
            Debug.Log("End of Game");
            gameOver();
        }
    }

    public void retry(){
        SceneManager.UnloadSceneAsync(GameManager.Instance.sceneName);
        SceneManager.LoadScene(GameManager.Instance.sceneName, LoadSceneMode.Additive);
    }

    public void gameOver(){
        Quizpanel.SetActive(false);
        GameoverPanel.SetActive(true);
        scoreTxt.text = score + "/" + totalquestions;
    }   
}
 