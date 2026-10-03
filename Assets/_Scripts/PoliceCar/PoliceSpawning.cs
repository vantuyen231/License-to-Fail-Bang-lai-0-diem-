using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSpawning : TuyenMonoBehaviour
{
    [SerializeField] protected PoliceSpawnCtrl policeSpawnCtrl;
    [SerializeField] protected LocalPointStreet selectedPoliceSpawnPoint;
    [SerializeField] protected int maxSpawn;
    [SerializeField] protected int policeActive = 0;
    [SerializeField] protected int spawnTimeLimit = 15;
    [SerializeField] protected int spawnTimeMission = 3;
    [SerializeField] protected float currentTime =0f;
    [SerializeField] protected bool isMission = false;

    [Header("List Police Active")]
    [SerializeField] protected List<PoliceCarCtrl> policeActiver = new List<PoliceCarCtrl>();

    private void FixedUpdate()
    {

        this.CheckCanSpawnPolice();
        if(isMission == true) return;
        this.DeSpawnPolice();
    }

    #region LoadComponent
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
    #endregion

    protected virtual void CheckActivePolice()
    {
        if (policeSpawnCtrl == null && policeSpawnCtrl.PoliceSpawner == null) return;

        int policeOff = this.policeSpawnCtrl.PoliceSpawner.InPoolObjs.Count;
        int policeOn = this.policeSpawnCtrl.PoliceSpawner.SpawnCount;
        policeActive = policeOn - policeOff;
    }

    public virtual void CheckCurrentPolice(int star)
    {
        if(isMission ==  true) return;
        maxSpawn = star;
    }

    protected virtual void CheckCanSpawnPolice()
    {
        //this.CheckCurrentPolice();
        this.CheckActivePolice();
        if (policeActive >= maxSpawn) return;
        currentTime += Time.deltaTime;
        int spawnTime = isMission ? spawnTimeMission : spawnTimeLimit;
        if (currentTime < spawnTime) return;
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
        this.policeActiver.Add(newPolice);
    }

    protected virtual void DeSpawnPolice()
    {
        if (policeActive <= maxSpawn) return;
        int indexDeSpawn = Random.Range(0, policeActiver.Count);
        PoliceCarCtrl policeDespawn = policeActiver[indexDeSpawn];
        this.policeSpawnCtrl.PoliceSpawner.Despawn(policeDespawn);
        policeActiver.Remove(policeActiver[indexDeSpawn]);
    }

    public virtual void DespawnAllActivePolice()
    {
        foreach (PoliceCarCtrl police in policeActiver)
        {
            if (police != null && police.gameObject.activeSelf)
            {
                this.policeSpawnCtrl.PoliceSpawner.Despawn(police);
            }
        }
        this.policeActiver.Clear();
        this.currentTime = 0f;
    }

    public virtual void SpawnPoliceInMission(int amountSpawn)
    {
        Debug.Log("Spawn Police: " + amountSpawn);
        this.DespawnAllActivePolice();
        isMission = true;
        maxSpawn = amountSpawn;

        currentTime = spawnTimeMission;
    }

    public virtual void ClearAllEndMission()
    {
        Debug.Log("Despawn Police");
        this.DespawnAllActivePolice();
        this.maxSpawn = 0;
        this.isMission = false;
    }

}
