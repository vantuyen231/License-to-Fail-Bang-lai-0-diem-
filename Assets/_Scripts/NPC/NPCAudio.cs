using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class NPCAudio : TuyenMonoBehaviour
{
    [SerializeField] protected AudioSource npcAudioSource;
    [SerializeField] protected string footStepNPC;
    [SerializeField] protected string hitNPC;
    [SerializeField] protected string hurtNPC;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAudioNPC();
    }

    private void LoadAudioNPC()
    {
        if(npcAudioSource != null) return;
        npcAudioSource = GetComponent<AudioSource>();
        Debug.Log(transform.name + ": LoadAudioNPC", gameObject);
    }

    public void FootStepSound()
    {
        AudioManager.Instance.SoundSFX(footStepNPC, npcAudioSource);
        Debug.Log( "Sound" + footStepNPC, transform);
    }

    public void HitNPC()
    {
        AudioManager.Instance.SoundSFX(hitNPC, npcAudioSource);
        Debug.Log("Sound" + hitNPC, transform);

    }

    public void HurtSound()
    {
        AudioManager.Instance.SoundSFX(hurtNPC, npcAudioSource);
        Debug.Log("Sound" + hurtNPC, transform);

    }
}
