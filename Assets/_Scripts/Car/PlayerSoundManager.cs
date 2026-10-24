using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : TuyenMonoBehaviour
{
    [SerializeField] protected SoundCarPlayer playerSound;

    [Header("Master Audio Cache")]
    [SerializeField] protected AudioClip startSound;
    [SerializeField] protected AudioClip brakeSound;
    [SerializeField] protected AudioClip hornSound;
    [SerializeField] protected List<AudioClip> engineLoopClips = new List<AudioClip>();

    [Header("Config Cache")]
    [SerializeField] protected float minRPM = 1000f;
    [SerializeField] protected float rpmStep = 500f;

    public AudioClip StartSound => startSound;
    public AudioClip BrakeSound => brakeSound;
    public AudioClip HornSound => hornSound;
    public IReadOnlyList<AudioClip> EngineLoopClips => engineLoopClips;
    public float MinRPM => minRPM;
    public float RPMStep => rpmStep;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSoundCarPlayer();
        this.LoadSoundInSO();
    }

    protected virtual void LoadSoundCarPlayer()
    {
        if (playerSound != null) return;
        playerSound = GetComponent<SoundCarPlayer>();
        Debug.Log(transform.name + ": LoadSoundCarPlayer", gameObject);
    }

    protected void LoadSoundInSO()
    {
        if (engineLoopClips.Count > 0) return;
        SoundCarPlayerSO so = playerSound.SoundCarPlayerSO;
        if (playerSound == null) return;
        startSound = so.startCarSound;
        brakeSound = so.brakeCarSound;
        hornSound = so.hornCarSound;
        minRPM = so.minRPM;
        rpmStep = so.rpmStep;

        this.engineLoopClips.Clear();
        if (so != null && so.engineSound != null)
        {
            engineLoopClips.AddRange(so.engineSound);
        }
    }
}
