using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class CarPlayerAudio : TuyenMonoBehaviour
{
    [SerializeField] protected SoundCarPlayer playerSound;
    [SerializeField] protected SoundCarPlayerSO soundCarPlayerSO;

    [SerializeField] protected Transform engineSoundHolder;
    [SerializeField] protected CarController carController;

    [SerializeField] protected UnityEngine.Audio.AudioMixerGroup audioMixer;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource brakeAudioSource;

    [SerializeField] protected int canBrakeSound = 25;

    [SerializeField] protected string startEngineSound = "StartCar";
    [SerializeField] protected string hitPlayerSound = "HitCar";
    [SerializeField] protected string hornPlayerSound = "HornCar";
    [SerializeField] protected string brakePlayerSound = "BrakeCar";
    [SerializeField] protected string enginePlayerSound = "EngineCar";
    [SerializeField] protected AudioSource[] engineCarSound;

    protected override void Start()
    {
        this.StartEngineSound();
        this.InitializeEngineCar();
    }

    private void Update()
    {
        this.UpdateEngineRPMBlend();
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarController();
        this.LoadSoundCarPlayer();
        this.SetUpAudioSound();
    }

    protected virtual void LoadCarController()
    {
        if (carController != null) return;
        carController = GetComponentInParent<CarController>();
        Debug.Log(transform.name + ": LoadCarController", gameObject);
    }

    protected virtual void LoadSoundCarPlayer()
    {
        if (playerSound != null) return;
        playerSound = GetComponent<SoundCarPlayer>();
        Debug.Log(transform.name + ": LoadSoundCarPlayer", gameObject);
        soundCarPlayerSO = playerSound.SoundCarPlayerSO;
    }

    protected void SetUpAudioSound()
    {
        AudioSource[] existingSources = GetComponents<AudioSource>();
        if (existingSources.Length < 1)
        {
            sfxAudioSource = gameObject.AddComponent<AudioSource>();
            brakeAudioSource = gameObject.AddComponent<AudioSource>();
        }
        else if (existingSources.Length < 2)
        {
            sfxAudioSource = existingSources[0];
            brakeAudioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            sfxAudioSource = existingSources[0];
            brakeAudioSource = existingSources[1];
        }
        sfxAudioSource.playOnAwake = false;
        sfxAudioSource.spatialBlend = 1f;
        sfxAudioSource.rolloffMode = AudioRolloffMode.Custom;
        sfxAudioSource.outputAudioMixerGroup = this.audioMixer;
        sfxAudioSource.maxDistance = 9f;

        if (engineSoundHolder != null) return;
        engineSoundHolder = transform.Find("EngineAudioSound");
        if (engineSoundHolder == null)
        {
            engineSoundHolder = new GameObject("EngineAudioSound").transform;
            engineSoundHolder.transform.parent = transform;
        }
    }
    #endregion

    private void InitializeEngineCar()
    {
        if (soundCarPlayerSO == null && engineSoundHolder == null) return;
        var engineDataCar = soundCarPlayerSO.GetSound(enginePlayerSound);
        if( engineDataCar == null && engineDataCar.clips.Count == 0) return;
        
        int engineClipCount = engineDataCar.clips.Count;
        engineCarSound = new AudioSource[engineClipCount];
        for (int i = 0; i < engineClipCount; i++)
        {
            AudioSource engineLayerSource = engineSoundHolder.gameObject.AddComponent<AudioSource>();

            engineLayerSource.loop = true;
            engineLayerSource.clip = engineDataCar.GetClip(i);
            engineLayerSource.maxDistance = 9f;
            engineLayerSource.playOnAwake = false;
            engineLayerSource.volume = engineDataCar.volume;
            engineLayerSource.rolloffMode = AudioRolloffMode.Custom;
            engineLayerSource.outputAudioMixerGroup = this.audioMixer;
            engineLayerSource.spatialBlend = 1;

            engineLayerSource.Play();

            engineCarSound[i] = engineLayerSource;

        }
    }

    private void UpdateEngineRPMBlend()
    {
        if (carController == null || engineCarSound == null || engineCarSound.Length == 0) return;

        float maxSpeed = 10f;
        float currentSpeed = carController.CarVeclocity;
        float normalizedRPM = Mathf.Clamp01(currentSpeed / maxSpeed);

        int count = engineCarSound.Length;

        if (count == 1)
        {
            engineCarSound[0].volume = 1f;
            engineCarSound[0].pitch = Mathf.Lerp(0.9f, 1.1f, normalizedRPM);
            return;
        }

        for (int i = 0; i < count; i++)
        {
            float centerPoint = (float)i / (count - 1);

            float distance = Mathf.Abs(normalizedRPM - centerPoint);

            float targetVolume = Mathf.Clamp01(1f - (distance * count));

            engineCarSound[i].volume = Mathf.Lerp(engineCarSound[i].volume, targetVolume, Time.deltaTime * 10f);

            engineCarSound[i].pitch = Mathf.Lerp(0.95f, 1.1f, normalizedRPM);
        }
    }

    private void StartEngineSound()
    {
        if(AudioManager.Instance == null) return;
        AudioManager.Instance.PlayerOneShotSound(soundCarPlayerSO, startEngineSound, sfxAudioSource);
    }

    public void HitCarNPCSound()
    {
        AudioManager.Instance.PlayerOneShotSound(soundCarPlayerSO, hitPlayerSound, sfxAudioSource);
    }

    public void BrakeCarSound()
    {
        if (carController.PlayerSpeed > canBrakeSound)
        {
            AudioManager.Instance.PlayerOneShotSound(soundCarPlayerSO, brakePlayerSound, brakeAudioSource);
        }
    }

    public void StopBrakeSound()
    {
        AudioManager.Instance.StopPlayerOneShotSound(soundCarPlayerSO, brakeAudioSource);
    }

    public void HornSound()
    {
        if (sfxAudioSource.isPlaying) return;
        AudioManager.Instance.PlayerOneShotSound(soundCarPlayerSO, hornPlayerSound, sfxAudioSource);
    }
}
