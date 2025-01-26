using UnityEngine;

public class PitchZone : MonoBehaviour
{
    [Range(1, 6)]
    public int playerNumber;
    
    private void Start()
    {
        if (GameManagerMauro.Instance)
        {
            if (playerNumber != GameManagerMauro.Instance.GetCurrentPlayer() + 1)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
