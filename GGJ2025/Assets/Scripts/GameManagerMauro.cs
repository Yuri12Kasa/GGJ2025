using System;
using UnityEngine;

public class GameManagerMauro : MonoBehaviour
{
    public static GameManagerMauro Instance;
    
    //Player
    public int playersNumber = 1;
    private int _currentPlayer = 1;

    public int GetCurrentPlayer()
    {
        return _currentPlayer;
    }
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextPlayer();
            SceneController.Instance.NextScene();
        }
    }

    public void NextPlayer()
    {
        _currentPlayer++;
    }
}
