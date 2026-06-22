using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnTrigger : TuyenMonoBehaviour
{
    [SerializeField] protected MaxSpawnTrigger maxSpawnTrigger;
    public MaxSpawnTrigger MaxSpawnTrigger => maxSpawnTrigger;
    [SerializeField] protected MinSpawnTrigger minSpawnTrigger;
    public MinSpawnTrigger MinSpawnTrigger => minSpawnTrigger;  

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMaxSpawnTrigger();
        this.LoadMinSpawnTrigger();
    }

    protected virtual void LoadMaxSpawnTrigger()
    {
        if(maxSpawnTrigger != null) return;
        maxSpawnTrigger = GetComponentInChildren<MaxSpawnTrigger>(); 
        Debug.Log(transform.name + ": LoadMaxSpawnTrigger", gameObject);

    }

    protected virtual void LoadMinSpawnTrigger()
    {
        if (minSpawnTrigger != null) return;
        minSpawnTrigger = GetComponentInChildren<MinSpawnTrigger>();
        Debug.Log(transform.name + ": LoadMinSpawnTrigger", gameObject);

    }

    protected virtual void OnTriggerExit(Collider other)
    {
        NPCCtrl npcCtrl = other.gameObject.GetComponentInChildren<NPCCtrl>();
        NPCDespawn npcDespawn = npcCtrl.GetComponentInChildren<NPCDespawn>();
        if (npcDespawn != null)
        {
            npcDespawn.OutAreaPlayer();
        }
    }
}
