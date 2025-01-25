using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    public int sampleWindow = 64;
    public AudioClip microphoneClip;

    private void Start()
    {
        MicrophoneToAudioClip();
    }

    public void MicrophoneToAudioClip()
    {
        string microphoneName = UnityEngine.Microphone.devices[0];
        microphoneClip = UnityEngine.Microphone.Start(microphoneName, true, 20, 44100);
    }

    public float GetLoudnessFromMicrophone()
    {
        return GetAudioLoudnessFromAudioClip(UnityEngine.Microphone.GetPosition(UnityEngine.Microphone.devices[0]), microphoneClip);
    }

    public float GetAudioLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0)
            return 0;
            
        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);
            
        float totalLoudness = 0.0f; 

        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }
            
        return totalLoudness / sampleWindow;
    }
}