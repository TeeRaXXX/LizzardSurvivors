using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public struct SoundClip
{
    public string Name;
    public AudioClip Clip;
}

[CreateAssetMenu(fileName = "SoundsSO", menuName = "NastyDoll/New SoundsList")]
public class SOSounds : ScriptableObject
{
    public List<SoundClip> SoundClips;
}