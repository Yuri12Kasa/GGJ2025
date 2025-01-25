using TMPro;
using UnityEngine;

public class GameManagerMauro : MonoBehaviour
{
    public static GameManagerMauro Instance;
    public TextMeshProUGUI finalText;
    [SerializeField] Microphone microphone;
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
