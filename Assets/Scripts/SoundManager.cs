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
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);

        foreach (var set in soundSets)
        {
            set.AudioSource.volume = 0.5f;
        }
    }

    public void PlaySFX(string name)
    {
        var soundSet = soundSets.Find(set => set.Name == "SFX");
        List <SoundClip> sounds = soundSet.Sounds.SoundClips.FindAll(clip => clip.Name == name);
        if (sounds.Count > 0 && sounds != null)
            soundSet.AudioSource.PlayOneShot(sounds[UnityEngine.Random.Range(0, sounds.Count - 1)].Clip);
    }
}