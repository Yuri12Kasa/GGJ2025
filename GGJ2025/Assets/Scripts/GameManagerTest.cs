using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class GameManagerTest : MonoBehaviour
{
    public TextMeshProUGUI finalText;
    [SerializeField] SpeechToText microphone;
   
    void Start()
    {
      microphone.SetString("firstPlayer");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            End();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            microphone.StartRecording();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            microphone.StopRecording();
        }
    }
    
    void End()
    {
        if (microphone.playersSpeech[0].Equals(microphone.playersSpeech[1]))
        {
          Debug.Log("Hai vinto sei fortissimo");
          finalText.text = "Hai vinto sei fortissimo";
          finalText.color = Color.magenta;
        }
        else
        {
            Debug.Log("Hai perso sei uno scarscone");
            finalText.text = "Hai perso sei uno scarscone";
            finalText.color = Color.red;
        }
    }
}
