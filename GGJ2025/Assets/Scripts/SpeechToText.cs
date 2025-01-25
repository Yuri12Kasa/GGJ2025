using System;
using System.IO;
//using HuggingFace.API;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpeechToText : MonoBehaviour
{   
    public TextMeshProUGUI text;
    [SerializeField] private int sampleRate = 44100;
    [SerializeField] private int lengthSec = 3599;
    public string[] playersSpeech;
    private string filePath;
    private AudioClip _recordedClip;
    private AudioSource _audioSource;
    private byte[] bytes;
    private bool _isRecording;
    private bool _firstRecord;
    public void SetString(string text)
    {
        filePath = "/" + text + ".wav";
    }
    private void Awake()
    {
        playersSpeech = new string[2];
        _firstRecord = true;
        _audioSource = GetComponent<AudioSource>();
       
    }

    public void StartRecording()
    {
        _recordedClip = UnityEngine.Microphone.Start(UnityEngine.Microphone.devices[0], false, lengthSec, sampleRate);
     
    }

    /*
    private void SendRecording() {
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
            text.color = Color.white;
            text.text = response;
            if (_firstRecord)
            {
                playersSpeech[0] = response;
                _firstRecord = false;
            }
            else
            {
                playersSpeech[1] = response;
               
               
            }
        }, error => {
            text.color = Color.red;
            text.text = error;
        });
    }

    */
    
    public void StopRecording()
    {
        var position = UnityEngine.Microphone.GetPosition(null);
        UnityEngine.Microphone.End(null);
        var samples = new float[position * _recordedClip.channels];
        _recordedClip.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, _recordedClip.frequency, _recordedClip.channels);
        _isRecording = false; 
        File.WriteAllBytes(Application.dataPath + filePath, bytes);
        //SendRecording();
    }
    public void PlayRecordedClip()
    {
        _audioSource.PlayOneShot(_recordedClip);
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
  
}
