using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Yuri
{
    public class TrackModifierManager : MonoBehaviour
    {
        public float modifierTimeSensibility = 0.01f;
        public AudioSource masterAudioSource;
        public AudioSource trackModifierAudioSource;
        public Track currentTrack;
        [SerializeField] private float _timer;
        private float _maxTime;
        private bool _trackIsPlaying;

        private int _index;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartTrack();
            }
            
            PlayingTrack();
        }

        private void StartTrack()
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
                _index = 0;
            }
            else
            {
                if (_index < currentTrack.trackModifiers.Count && _timer > currentTrack.trackModifiers[_index].time)
                {
                    float newPitch = currentTrack.trackModifiers[_index].pitch;
                    masterAudioSource.outputAudioMixerGroup.audioMixer.SetFloat("Pitch",
                        newPitch); 
                    trackModifierAudioSource.PlayOneShot(currentTrack.trackModifiers[_index].clip);
                    _index++;
                }
                _timer += Time.deltaTime;
            }
        }
        
    }
}
