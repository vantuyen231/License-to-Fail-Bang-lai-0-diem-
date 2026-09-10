using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionAvoidObstacles : BaseMission
{
    [Header("Mission Configs")]
    [SerializeField] protected float timeMission = 30f;
    [SerializeField] protected int scoreMission = 100;
    [SerializeField] protected float currentTimeTitle = 0f;
    [SerializeField] protected float currentTimeReady = 0f;
    [SerializeField] protected float currentTimeMission = 0f;
    [SerializeField] protected float timeIntroTittle = 3f;
    [SerializeField] protected float timeReadyMission = 3f;

    [Header("Mission States")]
    [SerializeField] protected bool isMissionActive = false;

    public static event Action<int> OnUpdateCountdown;
    public static event Action OnStartMission;
    protected override void Start()
    {

    }

    protected virtual void OnEnable()
    {
        PlayerScore.OnPlayerHit += HitDetection;
    }

    protected virtual void OnDisable()
    {
        PlayerScore.OnPlayerHit -= HitDetection;
    }

    protected virtual void HitDetection()
    {
        Debug.Log("Hit, mission fail");
    }

    protected virtual void SetReadyMission()
    {
        Debug.Log("Mission Title");
        currentTimeTitle += Time.deltaTime;
        if(currentTimeTitle <= timeIntroTittle) return;
        timeReadyMission -= Time.deltaTime;
        if (timeReadyMission > 0f) return;
        Debug.Log("Start Mision");


    }
}
