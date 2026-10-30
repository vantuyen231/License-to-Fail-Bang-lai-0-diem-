using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : TuyenSingleton<AudioManager>
{
    [SerializeField] protected AudioDatabaseSO audioDatabaseSO;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip lastMusic;
    private string currnetBgmGroupName;
    private Coroutine currentBgmCo;
    [SerializeField] private bool bgmShouldPlay;
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
            //bgmSource.loop = true;
        }
    }

    private void Update()
    {
        if (bgmSource.isPlaying == false && bgmShouldPlay)
        {
            if (string.IsNullOrEmpty(currnetBgmGroupName) == false)
            {
                NextBgm(currnetBgmGroupName);
            }

            if (bgmSource.isPlaying && bgmShouldPlay == false)
            {
                StopBgm();
            }
        }
    }
    public void SoundSFX(string soundSFX,  AudioSource targetAudioSource = null)
    {
        var audioSound = audioDatabaseSO.GetSound(soundSFX);
        if (audioSound == null) return;
        var clip = audioSound.GetRandomClip();

        AudioSource useAudioSource = targetAudioSource == null ? sfxSource : targetAudioSource;

        Debug.Log($"Dang phat sound [{soundSFX}] qua loa cua: {useAudioSource.gameObject.name}", useAudioSource.gameObject);

        useAudioSource.clip = clip;
        useAudioSource.pitch = Random.Range(0.95f, 1.1f);
        useAudioSource.volume = audioSound.volume;
        useAudioSource.PlayOneShot(clip);

    }



    public void StarBgm(string soundBgm)
    {
        bgmShouldPlay = true;
        if (soundBgm == currnetBgmGroupName)
        {
            return;
        }
        NextBgm(soundBgm);
    }

    public void NextBgm(string musicGroup)
    {
        bgmShouldPlay = true;
        currnetBgmGroupName = musicGroup;
        if (currentBgmCo != null)
        {
            StopCoroutine(currentBgmCo);
        }
        currentBgmCo = StartCoroutine(SwitchMusicCo(musicGroup));
    }

    public void StopBgm()
    {
        bgmShouldPlay = false;

        StartCoroutine(FadeVolumeCo(bgmSource, 0f, 1f));
        if (currentBgmCo != null)
        {
            StopCoroutine(currentBgmCo);
        }
    }

    private IEnumerator SwitchMusicCo(string musicGroup)
    {
        AudioClipData data = audioDatabaseSO.GetSound(musicGroup);
        AudioClip nextMusic = data.GetRandomClip();

        if (data == null || data.clips.Count == 0)
        {
            Debug.Log("No audio found for group" + musicGroup);
            yield return null;
        }

        if (data.clips.Count > 1)
        {
            while (nextMusic == lastMusic)
            {
                nextMusic = data.GetRandomClip();
            }
        }

        if (bgmSource.isPlaying)
        {
            yield return FadeVolumeCo(bgmSource, 0f, 1f);
        }

        lastMusic = nextMusic;
        bgmSource.clip = nextMusic;
        bgmSource.volume = 0f;
        bgmSource.Play();

        StartCoroutine(FadeVolumeCo(bgmSource, data.volume, 1f));
    }


    private IEnumerator FadeVolumeCo(AudioSource source, float targetVolume, float duration)
    {
        float time = 0f;
        float startVolume = source.volume;

        while (time < targetVolume)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, time/duration);
            yield return null;
        }

        source.volume = targetVolume;
    }
}
