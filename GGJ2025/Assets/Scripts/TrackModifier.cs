using System;
using UnityEngine;

namespace Yuri
{
    [Serializable]
    public class TrackModifier
    {
        public float time;
        [Range(0.5f, 2)]
        public float pitch = 1;
        public AudioClip clip;
    }
}