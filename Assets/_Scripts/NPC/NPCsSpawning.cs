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
    [SerializeField] protected PointPath selectedPoint;



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
            //Debug.Log("Null:" + npcCtrl + "||" + npcCtrl.Spawner);
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
        this.RandomChoisePointSpawn();
        if(selectedPoint == null) return;

        this.SpawnNPC();
        timer = 0;

    }

    protected virtual void RandomChoisePointSpawn()
    {
        this.selectedPoint = null;
        NPCSpawnTrigger.Instance.ChoiceNPCSpawnPoint();
        if (NPCSpawnTrigger.Instance == null || NPCSpawnTrigger.Instance.SpawnPoints.Count == 0) return;

        int randomIndex = Random.Range(0, NPCSpawnTrigger.Instance.SpawnPoints.Count);
        selectedPoint = NPCSpawnTrigger.Instance.SpawnPoints[randomIndex];
        //Debug.Log(selectedPoint);
    }

    protected virtual void SpawnNPC()
    {
        NPCCtrl npcPrefab = this.npcCtrl.Spawner.PoolPrefabs.GetRandom();
        NPCCtrl newNPC = this.npcCtrl.Spawner.Spawn(npcPrefab);

        newNPC.transform.position = this.selectedPoint.transform.position;
        newNPC.transform.rotation = this.selectedPoint.transform.rotation;

        newNPC.NpcRagdoll.DisableRagdoll();
        newNPC.SetActive(true);

        NPCMoving movingScript = newNPC.GetComponentInChildren<NPCMoving>();
        if (movingScript != null)
        {
            movingScript.enabled = true;
            movingScript.SetInitialPoint(this.selectedPoint);
        }
    }

}
