using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsSpawning : TuyenMonoBehaviour
{
    [SerializeField] protected NPCSpawnerCtrl npcCtrl;
    [SerializeField] protected float timer = 0f;
    [SerializeField] protected float delay = 3f;
    [SerializeField] protected int spawnMax = 5;
    [SerializeField] protected int spawnCount = 0;

    protected virtual void FixedUpdate()
    {
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

    protected virtual void RunTime()
    {
        if(spawnCount >= spawnMax) return;
        timer += Time.deltaTime;
        if(timer < delay ) return;
        timer = 0;

        NPCCtrl npcPrefab = this.npcCtrl.Spawner.PoolPrefabs.GetByName("NPC_0");
        NPCCtrl newNPC = this.npcCtrl.Spawner.Spawn(npcPrefab);
        newNPC.SetActive(true);

        spawnCount++;
    }
}
