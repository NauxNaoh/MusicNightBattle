using Naux.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [SerializeField] private List<Sounds> arrSound;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0, _count = arrSound.Count; i < _count; i++)
        {
            arrSound[i].audioSource = gameObject.AddComponent<AudioSource>();
            arrSound[i].audioSource.clip = arrSound[i].audioClip;
            arrSound[i].audioSource.playOnAwake = false;
            arrSound[i].audioSource.volume = arrSound[i].volume;

        }
    }

    public void PlayAudio(SoundType soundType, bool isLoop = false)
    {
        var _sound = arrSound.Find(x => x.soundType == soundType);
        if (_sound == null) return;

        _sound.audioSource.loop = isLoop;
        _sound.audioSource.Play();
    }

    public void StopAudio(SoundType soundType)
    {
        var _sound = arrSound.Find(x => x.soundType == soundType);
        _sound?.audioSource.Stop();
    }
}
[Serializable]
public class Sounds
{
    public SoundType soundType = SoundType.None;
    [Range(0, 1)] public float volume = 1.0f;
    public AudioClip audioClip;
    [HideInInspector] public AudioSource audioSource;
}

public enum SoundType
{
    None = 0,
    Count3 = 1,
    Count2 = 2,
    Count1 = 3,
    Go = 4,
    SongGame = 5,
}
