using System;
using System.Collections.Generic;
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

        private int index;

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
                index = 0;
            }
            else
            {
                if (index < currentTrack.trackModifiers.Count && _timer > currentTrack.trackModifiers[index].time)
                {
                    trackModifierAudioSource.PlayOneShot(currentTrack.trackModifiers[index].clip);
                    index++;
                }
                _timer += Time.deltaTime;
            }
        }
        
    }
    

    [Serializable]
    public class Track
    {
        public AudioClip clip;
        public List<TrackModifier> trackModifiers;

        public void SortList()
        {
            trackModifiers.Sort((x, y) => x.time.CompareTo(y.time));
        }
    }

    [Serializable]
    public class TrackModifier
    {
        public float time;
        public float pitch = 1;
        public AudioClip clip;
    }
        
}
