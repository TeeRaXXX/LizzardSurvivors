using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public struct SoundSet
{
    public string Name;
    public SOSounds Sounds;
    public AudioSource AudioSource;
}


public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<SoundSet> soundSets;

    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        foreach (var set in soundSets)
        {
            set.AudioSource.volume = 0.5f;
        }
    }

    public void PlaySFX(string name)
    {
        var soundSet = soundSets.Find(set => set.Name == "SFX");
        SoundClip sound = soundSet.Sounds.SoundClips.Find(clip => clip.Name == name);

        soundSet.AudioSource.PlayOneShot(sound.Clip);
    }
}