using UnityEngine;

public class GameManagerMauro : MonoBehaviour
{
    public static GameManagerMauro Instance;
    
    //Player
    public int playersNumber = 1;
    
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
}
