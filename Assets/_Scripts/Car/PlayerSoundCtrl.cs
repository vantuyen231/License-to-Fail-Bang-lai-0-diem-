using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundCtrl : TuyenMonoBehaviour
{
    [SerializeField] protected PlayerSoundManager playerSoundManager;

    [Header("--- DEDICATED AUDIO SOURCES ---")]
    [SerializeField] protected AudioSource startAudioSource;
    [SerializeField] protected AudioSource brakeAudioSource;
    [SerializeField] protected AudioSource hornAudioSource;

    [SerializeField] protected List<AudioSource> engineAudioSources = new List<AudioSource>();

    [Header("--- RUNTIME TELEMETRY (Test Values) ---")]
    [SerializeField] protected float currentRPM = 1000f;
    [SerializeField] protected float rpmDifference = 0f;
    [SerializeField] protected bool isBraking = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerSoundManager();
        this.LoadAndAssignSounds();
        this.SetupAllAudioSources();
    }

    protected virtual void LoadPlayerSoundManager()
    {
        if (playerSoundManager != null) return;
        playerSoundManager = GetComponent<PlayerSoundManager>();
        Debug.Log(transform.name + ": LoadPlayerSoundManager", gameObject);
    }
    protected virtual void SetupAllAudioSources()
    {
        if (playerSoundManager == null) return;

        // 1. Setup Start Audio Source
        if (startAudioSource == null)
        {
            startAudioSource = gameObject.AddComponent<AudioSource>();
            startAudioSource.playOnAwake = false;
            startAudioSource.spatialBlend = 1f;
        }
        if (playerSoundManager.StartSound != null)
        {
            startAudioSource.clip = playerSoundManager.StartSound;
        }

        // 2. Setup Brake Audio Source
        if (brakeAudioSource == null)
        {
            brakeAudioSource = gameObject.AddComponent<AudioSource>();
            brakeAudioSource.playOnAwake = false;
            brakeAudioSource.loop = true;
            brakeAudioSource.spatialBlend = 1f;
        }
        if (playerSoundManager.BrakeSound != null)
        {
            brakeAudioSource.clip = playerSoundManager.BrakeSound;
        }

        // 3. Setup Horn Audio Source
        if (hornAudioSource == null)
        {
            hornAudioSource = gameObject.AddComponent<AudioSource>();
            hornAudioSource.playOnAwake = false;
            hornAudioSource.spatialBlend = 1f;
        }
        if (playerSoundManager.HornSound != null)
        {
            hornAudioSource.clip = playerSoundManager.HornSound;
        }

        Debug.Log(transform.name + ": Start, Brake, Horn AudioSources!");
    }
    protected virtual void LoadAndAssignSounds()
    {
        var engineClips = playerSoundManager.EngineLoopClips;
        if (engineClips != null && engineClips.Count > 0)
        {
            if (engineAudioSources.Count == 0)
            {
                for (int i = 0; i < engineClips.Count; i++)
                {
                    AudioSource source = gameObject.AddComponent<AudioSource>();
                    source.clip = engineClips[i];
                    source.loop = true;
                    source.volume = 0f;
                    source.spatialBlend = 1f;

                    engineAudioSources.Add(source);
                }
            }
            else
            {
                for (int i = 0; i < engineAudioSources.Count && i < engineClips.Count; i++)
                {
                    if (engineAudioSources[i] != null)
                    {
                        engineAudioSources[i].clip = engineClips[i];
                        engineAudioSources[i].loop = true;
                    }
                }
            }
        }

        Debug.Log(transform.name + ":Add AudioSource Done!");
    }

    protected override void Start()
    {
        base.Start();
        this.InitEngineAudioSources();
        this.PlayStartEngineSound();
    }

    protected virtual void InitEngineAudioSources()
    {
        if (playerSoundManager == null) return;

        var clips = playerSoundManager.EngineLoopClips;
        if (clips == null || clips.Count == 0)
        {
            Debug.LogWarning(transform.name + ": Null Engine sound in PlayerSoundManager!", gameObject);
            return;
        }

        engineAudioSources.Clear();

        for (int i = 0; i < clips.Count; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = clips[i];
            source.loop = true;
            source.volume = 0f;
            source.spatialBlend = 1f; 
            source.playOnAwake = false;
            source.Play();

            engineAudioSources.Add(source);
        }

        Debug.Log(transform.name + ": Da khoi tao thanh cong " + engineAudioSources.Count + " AudioSource for Engine.");
    }

    protected virtual void PlayStartEngineSound()
    {
        if (playerSoundManager != null && playerSoundManager.StartSound != null && startAudioSource != null)
        {
            startAudioSource.PlayOneShot(playerSoundManager.StartSound);
        }
    }

    public virtual void SetCarAudioParameters(float rpm, bool brakingState)
    {
        this.currentRPM = rpm;
        this.isBraking = brakingState;
    }

    protected virtual void Update()
    {
        this.UpdateEngineCrossfade();
        this.UpdateBrakeSound();
    }

    protected virtual void UpdateEngineCrossfade()
    {
        if (playerSoundManager == null || engineAudioSources.Count == 0) return;

        float minRPM = playerSoundManager.MinRPM;
        float rpmStep = playerSoundManager.RPMStep;

        for (int i = 0; i < engineAudioSources.Count; i++)
        {
            float preferredRPM = minRPM + (i * rpmStep);
            rpmDifference = Mathf.Abs(currentRPM - preferredRPM);

            float volume = Mathf.Clamp01(1f - (rpmDifference / (rpmStep * 2f)));
            engineAudioSources[i].volume = volume;
            Debug.Log(engineAudioSources[i]);

            float pitch = currentRPM / preferredRPM;
            engineAudioSources[i].pitch = Mathf.Clamp(pitch, 0.5f, 2f);
        }
    }

    protected virtual void UpdateBrakeSound()
    {
        if (playerSoundManager == null || brakeAudioSource == null) return;

        if (isBraking)
        {
            if (!brakeAudioSource.isPlaying && playerSoundManager.BrakeSound != null)
            {
                brakeAudioSource.clip = playerSoundManager.BrakeSound;
                brakeAudioSource.Play();
            }
        }
        else
        {
            if (brakeAudioSource.isPlaying)
            {
                brakeAudioSource.Stop();
            }
        }
    }
}
