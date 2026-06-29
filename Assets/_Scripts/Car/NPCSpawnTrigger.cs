using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnTrigger : TuyenSingleton<NPCSpawnTrigger>
{
    [Header("Trigger")]
    [SerializeField] protected MaxSpawnTrigger maxSpawnTrigger;
    [SerializeField] protected MinSpawnTrigger minSpawnTrigger;

    [Header("List NPC Spawn Point")]
    [SerializeField] protected List<PointPath> spawnPoints = new List<PointPath>();
    public List<PointPath> SpawnPoints => spawnPoints;

    [Header("List CarNPC Spawn Point")]
    [SerializeField] protected List<LocalPointStreet> localPointStreets = new List<LocalPointStreet>();
    public List<LocalPointStreet> LocalPointStreet => localPointStreets;


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

    public virtual void ChoiceNPCSpawnPoint()
    {
        if (this.maxSpawnTrigger == null || this.minSpawnTrigger == null) return;

        this.spawnPoints.Clear();
        this.spawnPoints.AddRange(this.maxSpawnTrigger.PointsInMaxRange);

        foreach (PointPath minPoint in this.minSpawnTrigger.PointsInMinRanger)
        {
            if (this.spawnPoints.Contains(minPoint))
            {
                this.spawnPoints.Remove(minPoint);
            }
        }
    }

}
