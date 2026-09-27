using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionAvoidObstacles : TimeBaseMission
{
    [Header("Mission Configs")]
    [SerializeField] protected UINoDamgerMission uiMission;


    protected virtual void OnEnable()
    {
        PlayerScore.OnPlayerHit += HitDetection;
    }

    protected virtual void OnDisable()
    {
        PlayerScore.OnPlayerHit -= HitDetection;
    }

    #region LoadComponent
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIMission();
    }

    protected virtual void LoadUIMission()
    {
        if (this.uiMission != null) return;

        this.uiMission = FindObjectOfType<UINoDamgerMission>();
        Debug.Log(transform.name + ": LoadUIMission", gameObject);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.missionType = MissionType.NoViolation;
        this.missionName = "Avoid Obstacles";
        this.missionDescription = "Reach the destination without hitting any obstacles.";
    }
    #endregion

    protected virtual void HitDetection()
    {
        if (this.currentState != MissionState.ActiveGameplay) return;
        Debug.Log("Mision AO: mission fail");
        this.FinishMission(false);
    }

    protected override void OnTimeOut()
    {
        this.FinishMission(true);
    }

}
