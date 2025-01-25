using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yuri;
using Random = UnityEngine.Random;

public class GameManagerMauro : MonoBehaviour
{
    public static GameManagerMauro Instance;
    
    public float mainSceneTime = 20f;
    private float _time;

    public Track track;
    
    //Sentences
    [SerializeField] SentencesPool[] sentencesPool;
    private SentencesPool _activeSentencesPool;
    private Sentence _activeSentence;
    
    //Player
    [HideInInspector] public int playersNumber = 2;
    private int _currentPlayer = 1;
    
    public int GetCurrentPlayer() => _currentPlayer-1;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            SetPlayer(playersNumber);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayer(int playerNumber)
    {
        playersNumber = playerNumber;

        foreach (var pool in sentencesPool)
        {
            if(pool.players != playerNumber)
                continue;
            
            _activeSentencesPool = pool;
            break;
        }

        SetActiveSentence();
    }

    private void SetActiveSentence()
    {
        Debug.Log(_activeSentencesPool);
        var index = Random.Range(0, _activeSentencesPool.sentences.Length);
        _activeSentence = _activeSentencesPool.sentences[index];
    }

    public string GetSentence()
    {
        return _activeSentence.sentences[_currentPlayer - 1];
    }

    public void NextPlayer()
    {
        var nextPlayer = _currentPlayer + 1;
        
        if(nextPlayer > playersNumber)
            return;
        
        if(nextPlayer == playersNumber)
            SceneController.Instance.NextScene();
        
        _currentPlayer++;
        SceneController.Instance.PreviousScene();
    }

    public void AddTrackModifier(List<TrackModifier> modifiers)
    {
        track.trackModifiers.AddRange(modifiers);
    }

    public void CheckWin(TMP_Text text)
    {
        string sentence = text.text;
        sentence = sentence.Remove(sentence.Length - 1, 1);
        if (CompareStringToSentence(sentence))
        {
            Debug.Log("You win!");
        }
        else
        {
            Debug.Log("You lose!");
        }
    }

    private bool CompareStringToSentence(string sentence)
    {
        string correctSentence = "";
        
        for (int i = 0; i < _activeSentence.sentences.Length; i++)
        {
            if (i == _activeSentence.sentences.Length - 1)
            {
                correctSentence += _activeSentence.sentences[i];
            }
            else
            {
                correctSentence += _activeSentence.sentences[i] + " ";
            }
        }
        
        correctSentence = correctSentence.ToLower();
        sentence = sentence.ToLower();
        
        return sentence == correctSentence;
    }
}
