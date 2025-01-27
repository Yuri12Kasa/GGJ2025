using System.Collections;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Microphone : MonoBehaviour
{
    public float[] recTime = new float[6];

    public TMP_Text timerText;
    
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

    private float _currentRecTime;
    private float _timer;
    
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
            _currentRecTime = recTime[GameManagerMauro.Instance.GetCurrentPlayer()];
            timerText.text = _currentRecTime.ToString("0");
        }
        else
        {
            Debug.LogWarning("GameManagerMauro.Instance is null");
        }
    }

    private IEnumerator BlankRecord()
    {
        StartRecording();
        yield return new WaitForSeconds(0.5f);
        StopRecording();
    }

    private void Update()
    {
        if (_isRecording)
        {
            CheckTimer();
        }
        
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

    private void CheckTimer()
    {
        if (_timer > _currentRecTime)
        {
            StopRecording();
        }
        else
        {
            _timer += Time.deltaTime;
            timerText.text = (_currentRecTime - _timer).ToString("0");
        }
    }

    public void StartRecording()
    {
        // if (_recordedClip != null)
        //     return;
        OnStartRecording.Invoke();
        _isRecording = true;
        _recordedClip = UnityEngine.Microphone.Start(UnityEngine.Microphone.devices[0], false, lengthSec, sampleRate);
    }

    public void StopRecording()
    {
        OnEndRecording.Invoke();
        var position = UnityEngine.Microphone.GetPosition(UnityEngine.Microphone.devices[0]);
        UnityEngine.Microphone.End(UnityEngine.Microphone.devices[0]);
        var samples = new float[position * _recordedClip.channels];
        _recordedClip.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, _recordedClip.frequency, _recordedClip.channels);
        _isRecording = false; 
        //SendRecording();
        if (GameManagerMauro.Instance != null)
        {
            GameManagerMauro.Instance.SetTrackClip(_recordedClip);
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

    #region SpeechToText

    //private void SendRecording() {
    //    HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
    //        text.color = Color.white;
    //        text.text = response;
    //    }, error => {
    //        text.color = Color.red;
    //        text.text = error;
    //    });
    //}
    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels)
    {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2))
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
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

                foreach (var sample in samples)
                {
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