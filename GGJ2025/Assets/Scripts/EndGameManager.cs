using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EndGameManager : MonoBehaviour
{

    public TMP_Text inputText;
    public GameObject winImage;
    public GameObject loseImage;
    public TMP_Text correctSentenceText;
    
    
    public void CheckWin()
    {
        string sentence = inputText.text;
        sentence = sentence.Remove(sentence.Length - 1, 1);
        correctSentenceText.text = GetCorrectSentence();
        if (CompareStringToSentence(sentence))
        {
            winImage.SetActive(true);
            Debug.Log("You win!");
        }
        else
        {
            loseImage.SetActive(true);
            Debug.Log("You lose!");
        }
    }

    private bool CompareStringToSentence(string sentence)
    {
        var correctSentence = GetCorrectSentence();
        sentence = sentence.ToLower();
        
        return sentence == correctSentence;
    }

    private string GetCorrectSentence()
    {
        string correctSentence = "";

        Sentence activeSentence = GameManagerMauro.Instance.ActiveSentence;
        
        for (int i = 0; i < activeSentence.sentences.Length; i++)
        {
            if (i == activeSentence.sentences.Length - 1)
            {
                correctSentence += activeSentence.sentences[i];
            }
            else
            {
                correctSentence += activeSentence.sentences[i] + " ";
            }
        }
        
        correctSentence = correctSentence.ToLower();
        return correctSentence;
    }
}
