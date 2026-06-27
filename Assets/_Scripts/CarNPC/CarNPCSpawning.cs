using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPCSpawning : TuyenMonoBehaviour
{
    [SerializeField] protected float timer = 0;
    [SerializeField] protected float spawnTime = 2;
    [SerializeField] protected CarNPCSpawnCtrl carNPCSpawnCtrl;


    protected virtual void FixedUpdate()
    {
        this.TimeSystemSpawn();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarNPCSpawnCtrl();
    }

    protected virtual void LoadCarNPCSpawnCtrl()
    {
        if(this.carNPCSpawnCtrl != null) return;
        carNPCSpawnCtrl = GetComponent<CarNPCSpawnCtrl>();
        Debug.Log(transform.name + ": LoadCarNPCSpawnCtrl", gameObject);
    }
    protected virtual void TimeSystemSpawn()
    {
        this.timer += Time.fixedDeltaTime;
        if (this.timer < spawnTime) return;
        this.SpawnCarNPCs();
        this.timer = 0; 


    }

    protected virtual void SpawnCarNPCs()
    {
        Debug.Log("Spawn Car");
        CarNPCCtrl carNPCPrefab = this.carNPCSpawnCtrl.CarNPCSpawner.PoolPrefabs.GetByName("Car_NPC_0");
        CarNPCCtrl newCarNPC = this.carNPCSpawnCtrl.CarNPCSpawner.Spawn(carNPCPrefab);
    }
}
