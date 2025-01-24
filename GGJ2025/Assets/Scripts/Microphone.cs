using UnityEngine;

public class Microphone : MonoBehaviour
{
    [SerializeField] private int sampleRate = 44100;
    [SerializeField] private int lengthSec = 3599;
        
    private AudioClip _recordedClip;
    private AudioSource _audioSource;

    private bool _isRecording;
        
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void StartRecording()
    {
        _recordedClip = UnityEngine.Microphone.Start(UnityEngine.Microphone.devices[0], false, lengthSec, sampleRate);
    }

    public void StopRecording()
    {
        UnityEngine.Microphone.End(null);
    }

    public void PlayRecordedClip()
    {
        _audioSource.PlayOneShot(_recordedClip);
    }
}