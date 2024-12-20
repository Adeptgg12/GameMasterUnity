using System.Collections.Generic;
using UnityEngine;

public class WordBank : MonoBehaviour
{
    private List<string> originalWords = new List<string>() {
        "apple", "banana", "orange", "pineapple", "strawberry", "blueberry",
        "watermelon", "kiwi", "cherry", "mango", "coconut", "papaya",
        "grape", "lemon", "lime", "peach", "plum", "pear", "apricot",
        "avocado", "broccoli", "carrot", "lettuce", "spinach", "cucumber",
        "tomato", "onion", "garlic", "potato", "mushroom", "school",
        "teacher", "student", "library", "pencil", "eraser", "notebook",
        "classroom", "backpack", "blackboard", "river", "mountain",
        "forest", "ocean", "desert", "sunrise", "rainbow", "thunder",
        "snowfall", "waterfall"
    };

    private List<string> workingWords = new List<string>();

    private void Awake()
    {
        workingWords.AddRange(originalWords);
        Shuffle(workingWords);
        ConvertToLower(workingWords);
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

    private void ConvertToLower(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = list[i].ToLower();
        }
    }

    public string GetWord()
    {
        string newWord = string.Empty;
        if (workingWords.Count != 0)
        {
            newWord = workingWords[workingWords.Count - 1];
            workingWords.RemoveAt(workingWords.Count - 1);
        }
        return newWord;
    }
}
