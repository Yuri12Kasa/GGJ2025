using System;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    public Animator bubbleAnimExit;
    public Animator bubbleAnimEnter;

    private void Start()
    {
        PlayBubbleAnimEnter();
    }

    public void PlayBubbleAnimExit()
    {
        bubbleAnimExit.SetTrigger("Go");
    }
    
    public void PlayBubbleAnimEnter()
    {
        if (GameManagerMauro.Instance.GetCurrentPlayer() != 0)
        {
            bubbleAnimEnter.SetTrigger("Go");
        }
    }
}
