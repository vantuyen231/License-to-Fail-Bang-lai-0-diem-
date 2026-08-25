using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSpawning : TuyenMonoBehaviour
{
    [SerializeField] protected PoliceSpawnCtrl policeSpawnCtrl;
    [SerializeField] protected LocalPointStreet selectedPoliceSpawnPoint;
    [SerializeField] protected int maxSpawn;
    [SerializeField] protected float currentSpawn;
    [SerializeField] protected int policeActive = 0;
    [SerializeField] protected int spawnTimeLimit = 15;
    [SerializeField] protected float currentTime =0f;

    private void FixedUpdate()
    {
        this.CheckCanSpawnPolice();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoliceSpawnCtrl();
    }

    protected virtual void LoadPoliceSpawnCtrl()
    {
        if (this.policeSpawnCtrl != null) return;
        policeSpawnCtrl = GetComponent<PoliceSpawnCtrl>();
        Debug.Log(transform.name + ": LoadPoliceSpawnCtrl", gameObject);
    }

    protected virtual void SpawnPolicePoint()
    {
        this.selectedPoliceSpawnPoint = null;
        NPCSpawnTrigger.Instance.ChoiceCarNPCSpawnPoint();
        if (NPCSpawnTrigger.Instance == null || NPCSpawnTrigger.Instance.LocalPointStreet.Count == 0) return;
        int randomIndex = Random.Range(0, NPCSpawnTrigger.Instance.LocalPointStreet.Count);
        selectedPoliceSpawnPoint = NPCSpawnTrigger.Instance.LocalPointStreet[randomIndex];
    }

    protected virtual void CheckActivePolice()
    {
        if (policeSpawnCtrl == null && policeSpawnCtrl.PoliceSpawner == null) return;

        int policeOff = this.policeSpawnCtrl.PoliceSpawner.InPoolObjs.Count;
        int policeOn = this.policeSpawnCtrl.PoliceSpawner.SpawnCount;
        policeActive = policeOn - policeOff;


    }

    protected virtual void CheckCurrentPolice()
    {
        maxSpawn = GameManager.Instance.CurrentStars;
    }

    protected virtual void CheckCanSpawnPolice()
    {
        this.CheckCurrentPolice();
        this.CheckActivePolice();
        if (policeActive >= maxSpawn) return;
        currentTime += Time.deltaTime;
        if (currentTime < spawnTimeLimit) return;
        this.SpawnPolicePoint();
        if(selectedPoliceSpawnPoint == null) return;
        this.SpawnPolice();
        currentTime = 0f;
    }

    protected virtual void SpawnPolice()
    {
        PoliceCarCtrl policePrefab = this.policeSpawnCtrl.PoliceSpawner.PoolPrefabs.GetRandom();
        PoliceCarCtrl newPolice = this.policeSpawnCtrl.PoliceSpawner.Spawn(policePrefab);

        newPolice.transform.position = this.selectedPoliceSpawnPoint.transform.position;
        newPolice.transform.rotation = this.selectedPoliceSpawnPoint.transform.rotation;
        //Debug.Log("Spawn");

        AITargetPlayer aiPoliceScript = newPolice.GetComponentInChildren<AITargetPlayer>();
        PoliceCarMoving poliveMoving = newPolice.GetComponentInChildren<PoliceCarMoving>();
        if (aiPoliceScript != null)
        {
            aiPoliceScript.SetActive(true);
        }
        if (poliveMoving != null)
        {
            poliveMoving.LoadTargetPlayer();
        }
    }



}
