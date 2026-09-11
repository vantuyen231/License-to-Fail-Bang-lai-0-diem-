using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionAvoidObstacles : BaseMission
{
    [Header("Mission Configs")]
    [SerializeField] protected float timeMission = 30f;
    [SerializeField] protected int scoreMission = 100;
    [SerializeField] protected float currentTimeMission = 0f;
    [SerializeField] protected float timeIntroTittle = 3f;
    [SerializeField] protected float timeReadyMission = 3f;

    [SerializeField] protected int secondsLeft = 0;

    [SerializeField] protected float currentReady = 0f;



    [Header("Mission States")]
    [SerializeField] protected bool isMissionActive = false;

    public static event Action<int> OnUpdateCountdown;
    public static event Action OnStartMission;
    protected override void Start()
    {
        base.Start();
        this.StartMission();
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

    protected override void StartMission()
    {
        base.StartMission();
        this.isMissionActive = false;
        Debug.Log("StartMission");
        StartCoroutine(this.SetReadyMission());
    }
    protected virtual IEnumerator SetReadyMission()
    {
        Debug.Log("Show Title");
        yield return new WaitForSeconds(timeIntroTittle);
        currentReady = this.timeReadyMission;
        while (currentReady > 0)
        {
            secondsLeft = Mathf.CeilToInt(currentReady);

            OnUpdateCountdown?.Invoke(secondsLeft);

            yield return new WaitForSeconds(1f);
            currentReady -= 1f;
        }
        Debug.Log("Start Mision");
        this.isMissionActive = true;
        OnStartMission?.Invoke();
    }
}
