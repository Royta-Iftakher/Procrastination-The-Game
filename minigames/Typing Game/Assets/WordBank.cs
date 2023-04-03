using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WordBank : MonoBehaviour
{

    [SerializeField] private List<string> originalWords = new List<string>()
    {
        "Apple", "Tree", "Book", "Dog", "Cat", "Road", "Fish", "Table", "Coffee", "Window", "Pen", "Procastination"
    };

    [SerializeField] private List<string> workingWords = new List<string>();

    private void Awake() 
    {
        workingWords.AddRange(originalWords);
        Shuffle(workingWords);
    }

    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++) 
        {
            int random = Random.Range(i, list.Count);
            string temporary = list[i];

            list[i] = list[random];
            list[random] = temporary;
        }
    }

    public string GetWord()
    {
        string newWord = string.Empty;

        if (workingWords.Count !=0) 
        {
            newWord = workingWords.Last();
            workingWords.Remove(newWord);
        } 

        return newWord;
    }
}
