using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ingredients : MonoBehaviour
{
    [SerializeField] private TMP_Text[] textBoxes;
    [SerializeField] private string[] ingredients;

    public string[] randomizedArray;

    private void Start()
    {
        // Check if the number of available text boxes is less than 5
        if (textBoxes.Length < 5)
        {
            Debug.LogError("Insufficient number of UI TextBoxes!");
            return;
        }

        // Create a copy of the ingredients array to ensure no duplicates are used
        string[] randomizedIngredients = (string[])ingredients.Clone();

        // Randomly assign text to the text boxes from the randomized ingredients array
        randomizedArray = new string[5]; // Only 5 elements in the randomized array
        for (int i = 0; i < 5; i++)
        {
            int randomIndex = Random.Range(0, randomizedIngredients.Length);

            // Assign the ingredient to the text box
            textBoxes[i].text = randomizedIngredients[randomIndex];
            randomizedArray[i] = randomizedIngredients[randomIndex];

            // Remove the used ingredient from the randomized ingredients array to avoid duplicates
            randomizedIngredients[randomIndex] = randomizedIngredients[randomizedIngredients.Length - 1];
            System.Array.Resize(ref randomizedIngredients, randomizedIngredients.Length - 1);
        }

        // Print the randomized array to the console
        PrintRandomizedArray();
    }

    private void PrintRandomizedArray()
    {
        if (randomizedArray != null && randomizedArray.Length > 0)
        {
            Debug.Log("Randomized Array:");
            for (int i = 0; i < randomizedArray.Length; i++)
            {
                Debug.Log("Element " + (i + 1) + ": " + randomizedArray[i]);
            }
        }
        else
        {
            Debug.LogError("Randomized Array is empty!");
        }
    }
}
