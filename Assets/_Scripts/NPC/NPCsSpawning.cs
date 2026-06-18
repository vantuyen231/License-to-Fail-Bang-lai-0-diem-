using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsSpawning : TuyenMonoBehaviour
{
    [SerializeField] protected NPCSpawnerCtrl npcCtrl;
    [SerializeField] protected float timer = 0f;
    [SerializeField] protected float delay = 3f;
    [SerializeField] protected int spawnMax = 10;
    [SerializeField] protected int numNPCActive;
    [SerializeField] protected int numNPCOff;
    [SerializeField] protected int numNPCOn;
    

    protected virtual void FixedUpdate()
    {
        this.NPCActive();
        this.RunTime();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNPCSpawnerCtrl();
    }

    protected virtual void LoadNPCSpawnerCtrl()
    {
        if(npcCtrl != null) return;
        npcCtrl = GetComponent<NPCSpawnerCtrl>();
        Debug.Log(transform.name + ": LoadNPCSpawnerCtrl", gameObject);
    }

    protected virtual void NPCActive()
    {
        if (npcCtrl == null || npcCtrl.Spawner == null) 
        {
            Debug.Log("Null:" + npcCtrl + "||" + npcCtrl.Spawner);
            return;
        }
        numNPCOff = npcCtrl.Spawner.InPoolObjs.Count;
        numNPCOn = npcCtrl.Spawner.SpawnCount;
        numNPCActive = numNPCOn - numNPCOff;
    }

    protected virtual void RunTime()
    {
        
        if (numNPCActive >= spawnMax) return;
        timer += Time.deltaTime;
        if(timer < delay ) return;
        timer = 0;

        NPCCtrl npcPrefab = this.npcCtrl.Spawner.PoolPrefabs.GetByName("NPC_0");
        NPCCtrl newNPC = this.npcCtrl.Spawner.Spawn(npcPrefab);
        newNPC.NpcRagdoll.DisableRagdoll();
        newNPC.SetActive(true);

    }
}
