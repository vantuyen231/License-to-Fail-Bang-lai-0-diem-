using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : TuyenMonoBehaviour
{
    [SerializeField] protected CarController controller;
    [SerializeField] protected FrontBumpCtrl frontBumpCtrl;
    [SerializeField] protected MapTrigger mapTrigger;
    [SerializeField] protected NPCSpawnTrigger spawnTrigger;
    [SerializeField] protected PlayerScore playerScore;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarCtrl();
        this.LoandFrontBumpCtrl();
        this.LoadMapTrigger();
        this.LoadNPCSpawnTrigger();
        this.LoadPlayerScore();
    }

    private void LoadNPCSpawnTrigger()
    {
        if(spawnTrigger != null) return;
        spawnTrigger = GetComponentInChildren<NPCSpawnTrigger>();
        Debug.Log(transform.name + ": NPCSpawnTrigger", gameObject);
    }

    protected virtual void LoadCarCtrl()
    {
        if(controller != null) return;
        controller = GetComponent<CarController>();
    }

    protected virtual void LoandFrontBumpCtrl()
    {
        if (frontBumpCtrl != null) return;
        frontBumpCtrl = GetComponentInChildren<FrontBumpCtrl>();
    }

    protected virtual void LoadMapTrigger()
    {
        if (mapTrigger != null) return;
        mapTrigger = GetComponentInChildren<MapTrigger>();
    }

    protected virtual void LoadPlayerScore()
    {
        if (playerScore != null) return;
        playerScore = GetComponentInChildren<PlayerScore>();
        Debug.Log(transform.name + ": LoadPlayerScore", gameObject );
    }
}
