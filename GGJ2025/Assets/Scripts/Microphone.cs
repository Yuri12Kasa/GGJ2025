using System.IO;
using System.Linq;

using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Microphone : MonoBehaviour
{

    public UnityEvent OnStartRecording;
    public UnityEvent OnEndRecording;
    
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
    [SerializeField] private bool _isRecording;
    private bool _firstRecord;
    
    private void Awake()
    {
        _firstRecord = true;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (GameManagerMauro.Instance != null)
        {
            _playersNum = GameManagerMauro.Instance.playersNumber;
            playersSpeech = new string[2];
        }
        else
        {
            Debug.LogWarning("GameManagerMauro.Instance is null");
        }
    }

    private void Update()
    {
        if (_recordedClip != null)
            return;
        
        if (!_isRecording)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("StartRecord");
                StartRecording();
            }
        }
        else
        {
            if (!Input.GetKeyUp(KeyCode.Space))
            {
                StopRecording();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayRecordedClip();
        }
    }

    public void StartRecording()
    {
        if (_recordedClip != null)
            return;
        OnStartRecording.Invoke();
        _isRecording = true;
        _recordedClip = UnityEngine.Microphone.Start(UnityEngine.Microphone.devices[0], false, lengthSec, sampleRate);
    }

    public void StopRecording()
    {
        OnEndRecording.Invoke();
        var position = UnityEngine.Microphone.GetPosition(null);
        UnityEngine.Microphone.End(null);
        var samples = new float[position * _recordedClip.channels];
        _recordedClip.GetData(samples, sampleRate);
        //sbytes = EncodeAsWAV(samples, _recordedClip.frequency, _recordedClip.channels);
        _isRecording = false; 
        //SendRecording();
        if (GameManagerMauro.Instance != null)
        {
            if (GameManagerMauro.Instance.playersNumber == GameManagerMauro.Instance.GetCurrentPlayer())
            {
                CheckText();
            }
        }
    }

    public void PlayRecordedClip()
    {
        Debug.Log("PlayRecordedClip");
        _audioSource.clip = _recordedClip;
        _audioSource.Play();
    }

    public void NextScene()
    {
        if (SceneController.Instance)
        {
            SceneController.Instance.NextScene();
        }
    }

   /* #region SpeechToText

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
   */
    
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