using System;
using UnityEngine;

[Serializable]
public class TrackModifier
{
    [Range(0, 1)]
    public float time;
    [Range(0.5f, 2)]
    public float pitch = 1;
    public AudioClip clip;
}