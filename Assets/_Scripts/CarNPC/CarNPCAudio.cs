using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPCAudio : TuyenMonoBehaviour
{
    [SerializeField] protected VehicalAudioSO vehicalAudioSO;
    [SerializeField] protected AudioSource loopAudioSource;   
    [SerializeField] protected AudioSource oneShotAudioSource;
    [SerializeField] protected string engineSound;
    [SerializeField] protected string hornSound;
    [SerializeField] protected string hitSound;

    [SerializeField] protected bool isEmergencyVehicle = false;
    [SerializeField] protected string sirenSoundKey;


    protected override void Start()
    {
        this.PlayCarAudioSetup();
    }
    public virtual void PlayCarAudioSetup()
    {
        AudioManager.Instance.CarNPCLoopSound(vehicalAudioSO, engineSound, loopAudioSource);

        if (isEmergencyVehicle && !string.IsNullOrEmpty(sirenSoundKey))
        {
            AudioManager.Instance.CarNPCLoopSound(vehicalAudioSO, sirenSoundKey, loopAudioSource);
        }
    }

    public void PlayHorn()
    {
        AudioManager.Instance.PlayVehicleOneShot(vehicalAudioSO, hornSound, oneShotAudioSource);
    }

    public void PlayHit()
    {
        AudioManager.Instance.PlayVehicleOneShot(vehicalAudioSO, hitSound, oneShotAudioSource);
    }
}
