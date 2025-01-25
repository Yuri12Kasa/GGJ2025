using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Yuri
{
    public class TrackModifierManager : MonoBehaviour
    {
        public AudioSource masterAudioSource;
        public AudioSource trackModifierAudioSource;
        public Track currentTrack;
        [SerializeField] private float _timer;
        private float _maxTime;
        private bool _trackIsPlaying;
        [SerializeField] private List<TrackModifier> _currentTrackModifiers;

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
            _currentTrackModifiers = currentTrack.trackModifiers;
            
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
            }
            else
            {
                _timer += Time.deltaTime;
                foreach (var trackModifier in _currentTrackModifiers)
                {
                    if (Mathf.Abs(trackModifier.time - _timer) < 0.3)
                    {
                        trackModifierAudioSource.PlayOneShot(trackModifier.clip);
                    }
                }
            }
        }
        
    }
    

    [Serializable]
    public class Track
    {
        public AudioClip clip;

        public List<TrackModifier> trackModifiers;
    }

    [Serializable]
    public class TrackModifier
    {
        public float time;
        public float pitch = 1;
        public AudioClip clip;
    }
        
}
