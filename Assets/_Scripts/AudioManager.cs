using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : TuyenSingleton<AudioManager>
{
    [SerializeField] protected AudioDatabaseSO audioDatabaseSO;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    protected override void Awake()
    {
        base.Awake();

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void SoundSFX(string soundSFX)
    {
        var audioSound = audioDatabaseSO.GetSound(soundSFX);
        if (audioSound == null) return;
        var clip = audioSound.GetRandomClip();
        sfxSource.clip = clip;
        sfxSource.volume = audioSound.volume;
        sfxSource.PlayOneShot(clip);

    }
}
