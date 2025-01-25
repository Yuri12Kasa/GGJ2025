using System;
using System.Collections.Generic;
using UnityEngine;

namespace Yuri
{
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
}