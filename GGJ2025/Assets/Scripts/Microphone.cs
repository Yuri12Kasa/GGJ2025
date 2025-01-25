using System;
using System.IO;
using System.Linq;
using HuggingFace.API;
using TMPro;
using UnityEngine;

public class Microphone : MonoBehaviour
{
    
    [Header("Debug Texts")]
    [SerializeField] public TextMeshProUGUI text;
    [Header("Record Value")]
    [SerializeField] private int sampleRate = 44100;
    [SerializeField] private int lengthSec = 3599;
        
    private AudioClip _recordedClip;
    private AudioSource _audioSource;
    public string[] playersSpeech;

    private int _playersNum;
    private byte[] bytes;
    private bool _isRecording;
    private bool _firstRecord;
    private void Awake()
    {
        _firstRecord = true;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _playersNum = GameManagerMauro.Instance.playersNumber;
        playersSpeech = new string[2];
    }

    public void StartRecording()
    {
        _recordedClip = UnityEngine.Microphone.Start(UnityEngine.Microphone.devices[0], false, lengthSec, sampleRate);
    }

    public void StopRecording()
    {
        var position = UnityEngine.Microphone.GetPosition(null);
        UnityEngine.Microphone.End(null);
        var samples = new float[position * _recordedClip.channels];
        _recordedClip.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, _recordedClip.frequency, _recordedClip.channels);
        _isRecording = false; 
        SendRecording();
        if (GameManagerMauro.Instance.playersNumber == GameManagerMauro.Instance.GetCurrentPlayer())
        {
            CheckText();
        }
    }

    public void PlayRecordedClip()
    {
        _audioSource.PlayOneShot(_recordedClip);
    }

    #region SpeechToText

    private void SendRecording() {
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
            text.color = Color.white;
            text.text = response;
        }, error => {
            text.color = Color.red;
            text.text = error;
        });
    }
    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels) {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2)) {
            using (var writer = new BinaryWriter(memoryStream)) {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)channels);
                writer.Write(frequency);
                writer.Write(frequency * channels * 2);
                writer.Write((ushort)(channels * 2));
                writer.Write((ushort)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);
    
                foreach (var sample in samples) {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            return memoryStream.ToArray();
        }
    }

    #endregion
    
    void CheckText()
    {
        if (playersSpeech[0].Equals(playersSpeech.Last()))
        {
            ShowEndLevel.Instance.ShowText("Hai vinto sei fortissimo", Color.magenta);
        }
        else
        {
            ShowEndLevel.Instance.ShowText("Hai perso sei uno scarscone", Color.red);
        }
    }
}