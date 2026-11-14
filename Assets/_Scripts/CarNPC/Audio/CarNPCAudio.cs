using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CarNPCAudio : TuyenMonoBehaviour
{
    [SerializeField] protected CarNPCCtrl carNPCCtrl;
    [SerializeField] protected VehicalAudioSO vehicalAudioSO;
    [SerializeField] protected AudioSource loopAudioSource;   
    [SerializeField] protected AudioSource oneShotAudioSource;
    [SerializeField] protected string engineSound = "EngineCarNPC";
    [SerializeField] protected string hornSound = "HornCarNPC";
    [SerializeField] protected string hitSound = "HitCarNPC";

    [SerializeField] protected bool isEmergencyVehicle = false;
    [SerializeField] protected string sirenSoundKey;


    protected override void Start()
    {
        this.PlayCarAudioSetup();
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarNPCCtrl();
        this.LoadCarNPCSO();
        this.LoadCarNPCSFXSound();
        this.LoadLoopAudioSound();
    }

    protected virtual void LoadCarNPCCtrl()
    {
        if (carNPCCtrl != null) return;
        carNPCCtrl = GetComponentInParent<CarNPCCtrl>();
        Debug.Log(transform.name + ": LoadCarNPCCtrl", gameObject);
    }

    protected virtual void LoadCarNPCSO()
    {
        if (vehicalAudioSO != null) return;
        vehicalAudioSO = carNPCCtrl.CarNPCInfo.CarSoundSO;
        Debug.Log(transform.name + ": LoadCarNPCSO", gameObject);
    }

    protected virtual void LoadCarNPCSFXSound()
    {
        if(oneShotAudioSource != null) return;
        CarNPCSFXAudio sfxScript = GetComponentInChildren<CarNPCSFXAudio>();
        if (sfxScript != null)
        {
            oneShotAudioSource = sfxScript.GetComponent<AudioSource>();
            Debug.Log(transform.name + ": LoadCarNPCSFXSound", gameObject);
        }

    }

    protected void LoadLoopAudioSound()
    {
        if (loopAudioSource != null) return;
        CarNPCLoopAudio loopChild = GetComponentInChildren<CarNPCLoopAudio>();
        if (loopChild != null)
        {
            loopAudioSource = loopChild.GetComponent<AudioSource>();
            Debug.Log(transform.name + ": LoadCarNPCLoopSound", gameObject);
        }

    }
    #endregion

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
