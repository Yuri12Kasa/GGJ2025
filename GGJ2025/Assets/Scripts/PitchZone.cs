using UnityEngine;

public class PitchZone : MonoBehaviour
{
    public int playerNumber;
    
    private void Start()
    {
        if (GameManagerMauro.Instance)
        {
            if (playerNumber != GameManagerMauro.Instance.playersNumber)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
