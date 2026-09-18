using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionAvoidObstacles : BaseMission
{
    [Header("Mission Configs")]
    [SerializeField] protected float timeMission = 30f;
    [SerializeField] protected float timer = 0f;
    [SerializeField] protected int scoreMission = 100;
    [SerializeField] protected float currentTimeMission = 0f;
    [SerializeField] protected float timeIntroTittle = 3f;
    [SerializeField] protected float timeReadyMission = 3f;

    [SerializeField] protected float secondsLeft = 0;

    [SerializeField] protected float currentReady = 0f;



    [Header("Mission States")]
    [SerializeField] protected bool isMissionActive = false;
    [SerializeField] protected MissionState currentState = MissionState.None;


    public static event Action<MissionState> UpdateStateMission;
    public static event Action<float> OnTimeTick;

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
        if (this.currentState != MissionState.ActiveGameplay) return;
        this.ChangeState(MissionState.Failed);
        StopAllCoroutines();
        isMissionActive = false;
        //Debug.Log("Hit, mission fail");
        this.FinishMission(false);
    }

    protected override void StartMission()
    {
        base.StartMission();
        this.isMissionActive = false;
        StartCoroutine(this.SetReadyMission());
    }

    protected virtual IEnumerator SetReadyMission()
    {
        yield return StartCoroutine(this.TitleMission());
        yield return StartCoroutine(this.CountDownMission());
        yield return StartCoroutine(this.ActiveMission());
    }

    protected virtual IEnumerator TitleMission()
    {
        this.ChangeState(MissionState.TitleStage);
        yield return new WaitForSeconds(timeIntroTittle);
    }

    protected virtual IEnumerator CountDownMission()
    {
        this.ChangeState(MissionState.CountdownStage);
        currentReady = this.timeReadyMission;
        while (currentReady > 0)
        {
            OnTimeTick?.Invoke(Mathf.CeilToInt(currentReady));
            yield return new WaitForSeconds(1f);
            currentReady -= 1f;
        }
        currentReady = 0;
    }

    protected virtual IEnumerator ActiveMission()
    {

        this.ChangeState(MissionState.ActiveGameplay);
        this.isMissionActive = true;

        secondsLeft = this.timeMission;
        while (secondsLeft > 0 && this.currentState == MissionState.ActiveGameplay)
        {
            OnTimeTick?.Invoke(Mathf.Max(0f, secondsLeft));
            yield return null;
            secondsLeft -= Time.deltaTime;
        }
        if (this.currentState == MissionState.ActiveGameplay)
        {
            OnTimeTick?.Invoke(0.0f);
            this.SuccessMission();

        }
    }

    protected virtual void SuccessMission()
    {
        this.ChangeState(MissionState.Success);
        isMissionActive = false;
        Debug.Log("Done Mission");
        this.FinishMission(true);
    }



    protected virtual void ChangeState(MissionState state)
    {
        this.currentState = state;
        UpdateStateMission?.Invoke(state);
    }
}
