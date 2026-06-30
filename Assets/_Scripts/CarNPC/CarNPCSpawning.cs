using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPCSpawning : TuyenMonoBehaviour
{
    [SerializeField] protected CarNPCSpawnCtrl carNPCSpawnCtrl;
    [SerializeField] protected LocalPointStreet selectedCarNPCPoint;
    [SerializeField] protected float timer = 0;
    [SerializeField] protected float spawnTime = 2;
    [SerializeField] protected int maxSpawn = 20;
    [SerializeField] protected int carNPCActive;
    [SerializeField] protected int carNPCOff;
    [SerializeField] protected int carNPCOn;



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

    protected virtual void CheckCarNPCActive()
    {
        if(carNPCSpawnCtrl == null && carNPCSpawnCtrl.CarNPCSpawner == null) return;

        carNPCOff = carNPCSpawnCtrl.CarNPCSpawner.InPoolObjs.Count;
        carNPCOn = carNPCSpawnCtrl.CarNPCSpawner.SpawnCount;
        carNPCActive = carNPCOn - carNPCOff;
    }
    protected virtual void TimeSystemSpawn()
    {
        if(carNPCActive >= maxSpawn) return;
        this.timer += Time.fixedDeltaTime;
        if (this.timer < spawnTime) return;
        this.RandomCarNPCSpawn(); 
        if (selectedCarNPCPoint == null) return;
        this.SpawnCarNPCs();
        this.timer = 0; 


    }

    protected virtual void RandomCarNPCSpawn()
    {
        this.selectedCarNPCPoint = null;
        NPCSpawnTrigger.Instance.ChoiceCarNPCSpawnPoint();
        if (NPCSpawnTrigger.Instance == null || NPCSpawnTrigger.Instance.LocalPointStreet.Count == 0) return;

        int randomIndex = Random.Range(0, NPCSpawnTrigger.Instance.LocalPointStreet.Count);
        selectedCarNPCPoint = NPCSpawnTrigger.Instance.LocalPointStreet[randomIndex];
    }

    protected virtual void SpawnCarNPCs()
    {
        Debug.Log("Spawn Car");

        CarNPCCtrl carNPCPrefab = this.carNPCSpawnCtrl.CarNPCSpawner.PoolPrefabs.GetByName("Car_NPC_0");
        CarNPCCtrl newCarNPC = this.carNPCSpawnCtrl.CarNPCSpawner.Spawn(carNPCPrefab);

        newCarNPC.transform.position = this.selectedCarNPCPoint.transform.position;
        newCarNPC.transform.rotation = this.selectedCarNPCPoint.transform.rotation;
        
        

        CarNPCMoving movingCarNPCScript = newCarNPC.GetComponentInChildren<CarNPCMoving>();
        if (movingCarNPCScript != null)
        {
            movingCarNPCScript.SetActive(true);
            movingCarNPCScript.SetStartPointCarNPC(this.selectedCarNPCPoint);
        }
    }
}
