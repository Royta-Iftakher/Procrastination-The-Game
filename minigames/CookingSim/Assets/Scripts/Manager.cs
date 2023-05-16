using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] TMP_Text IngredientsTextBox;
    [SerializeField] GameObject firstPage;
    [SerializeField] GameObject secondPage;
    [SerializeField] GameObject thirdPage;
    [SerializeField] GameObject fourthPage;
    [SerializeField] GameObject ErrorScreen;
    [SerializeField] GameObject gameWon;
    [SerializeField] GameObject gameLost;
    [SerializeField] GameObject[] gameOverObjects;
    public List<string> SubmittedIngredients = new List<string>();

    // Reference to the Ingredients script
    private Ingredients ingredientsScript;

    private void Start()
    {
        // Get the Ingredients script component attached to the Ingredients game object
        ingredientsScript = FindObjectOfType<Ingredients>();
    }

    public void submitIngredient(string buttonName)
    {
        if (SubmittedIngredients.Count >= 5)
        {
            ErrorScreen.SetActive(true);
            return;
        }

        SubmittedIngredients.Add(buttonName);
        PrintSubmittedIngredients();
    }

    public void ClearSubmittedIngredients()
    {
        SubmittedIngredients.Clear();
        PrintSubmittedIngredients();
    }

    private void PrintSubmittedIngredients()
    {
        IngredientsTextBox.text = "Submitted Ingredients: " + string.Join(", ", SubmittedIngredients.ToArray());
    }

    public void CheckSubmission()
    {
        // Sort both lists for comparison (to ignore the order)
        SubmittedIngredients.Sort();
        List<string> randomizedIngredientsList = new List<string>(ingredientsScript.randomizedArray);
        randomizedIngredientsList.Sort();

        // Compare the sorted lists
        bool isSubmissionCorrect = true;

        if (SubmittedIngredients.Count != randomizedIngredientsList.Count)
        {
            isSubmissionCorrect = false;
        }
        else
        {
            for (int i = 0; i < SubmittedIngredients.Count; i++)
            {
                if (SubmittedIngredients[i] != randomizedIngredientsList[i])
                {
                    isSubmissionCorrect = false;
                    break;
                }
            }
        }

        GameObject[] overObjects = GameObject.FindGameObjectsWithTag("over");
            foreach (GameObject obj in overObjects)
            {
                obj.SetActive(false);
            }

       if (isSubmissionCorrect)
        {
            gameWon.SetActive(true);
        }
        else
        {
           gameLost.SetActive(true);
        }
    }

    public void gotoFirstPage()
    {
        firstPage.SetActive(true);
        secondPage.SetActive(false);
        thirdPage.SetActive(false);
        fourthPage.SetActive(false);
    }

    public void gotoSecondPage()
    {
        firstPage.SetActive(false);
        secondPage.SetActive(true);
        thirdPage.SetActive(false);
        fourthPage.SetActive(false);
    }

    public void gotoThirdPage()
    {
        firstPage.SetActive(false);
        secondPage.SetActive(false);
        thirdPage.SetActive(true);
        fourthPage.SetActive(false);
    }

    public void gotoFourthPage()
    {
        firstPage.SetActive(false);
        secondPage.SetActive(false);
        thirdPage.SetActive(false);
        fourthPage.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    
    }
}
