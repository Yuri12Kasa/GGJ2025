using UnityEngine;
using Yuri;

public class GameManagerMauro : MonoBehaviour
{
    public static GameManagerMauro Instance;
    
    public float mainSceneTime = 20f;

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
            Destroy(this);
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
        _currentPlayer++;
    }
}
