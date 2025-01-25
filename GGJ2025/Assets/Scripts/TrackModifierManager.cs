using UnityEngine;
using Yuri;

public class TrackModifierManager : MonoBehaviour
{
    public AudioSource masterAudioSource;
    public AudioSource trackModifierAudioSource;
    public Track currentTrack;
    [SerializeField] private float _timer;
    private float _maxTime;
    private bool _trackIsPlaying;

    private int _modifierIndex;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartTrack();
        }
            
        PlayingTrack();
    }

    public void StartTrack()
    {
        if (_trackIsPlaying)
            return;
        _trackIsPlaying = true;
        _maxTime = currentTrack.clip.length;
        masterAudioSource.clip = currentTrack.clip;
        currentTrack.SortList();
        masterAudioSource.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", 1); 
            
        masterAudioSource.Play();
    }

    private void PlayingTrack()
    {
        if (!_trackIsPlaying)
            return;
        if (_timer > _maxTime)
        {
            _trackIsPlaying = false;
            _timer = 0;
            _modifierIndex = 0;
        }
        else
        {
            if (_modifierIndex < currentTrack.trackModifiers.Count)
            {
                float modifierTime = Mathf.Lerp(0, _maxTime, currentTrack.trackModifiers[_modifierIndex].time);
                if (_timer > modifierTime)
                {
                    float newPitch = currentTrack.trackModifiers[_modifierIndex].pitch;
                    masterAudioSource.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", newPitch);
                    if (currentTrack.trackModifiers[_modifierIndex].clip)
                    {
                        trackModifierAudioSource.PlayOneShot(currentTrack.trackModifiers[_modifierIndex].clip);
                    }
                    _modifierIndex++;
                }
            }
            _timer += Time.deltaTime;
        }
    }
        
}