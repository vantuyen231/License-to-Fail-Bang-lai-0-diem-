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

    [SerializeField] protected int secondsLeft = 0;

    [SerializeField] protected float currentReady = 0f;



    [Header("Mission States")]
    [SerializeField] protected bool isMissionActive = false;
    [SerializeField] protected MissionState currentState = MissionState.None;

    //public static event Action SetUpMission;
    //public static event Action ShowNumStage;
    //public static event Action<int> OnUpdateCountdown;
    //public static event Action OnStartMission;
    //public static event Action<float> UpdateCountDownMission;

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
        this.ChangeState(MissionState.Failed);
        Debug.Log("Hit, mission fail");
    }

    protected override void StartMission()
    {
        base.StartMission();
        //SetUpMission?.Invoke();
        this.isMissionActive = false;
        StartCoroutine(this.SetReadyMission());
    }
    protected virtual IEnumerator SetReadyMission()
    {
        yield return StartCoroutine(this.TitleMission());
        //SetUpMission?.Invoke();
        //yield return new WaitForSeconds(timeIntroTittle);
        //ShowNumStage?.Invoke();
        yield return StartCoroutine(this.CountDownMission());
        //currentReady = this.timeReadyMission;
        //while (currentReady > 0)
        //{
        //    secondsLeft = Mathf.CeilToInt(currentReady);

        //    OnUpdateCountdown?.Invoke(secondsLeft);

        //    yield return new WaitForSeconds(1f);
        //    currentReady -= 1f;
        //}
        //Debug.Log("Start Mission");
        //this.isMissionActive = true;
        //OnStartMission?.Invoke();
        yield return StartCoroutine(this.ActiveMission());

        //yield return StartCoroutine(this.CurrentEndMission());


    }

    //protected virtual IEnumerator CurrentEndMission()
    //{

    //    timer = timeMission;
    //    while (timer >= 0)
    //    {
    //        timer -= Time.deltaTime;
    //        UpdateCountDownMission?.Invoke(timer);
    //        yield return null;
    //    }

    //    this.timer = 0f;
    //    this.isMissionActive = false;
    //}

    protected virtual IEnumerator TitleMission()
    {
        this.ChangeState(MissionState.TitleStage);
        yield return new WaitForSeconds(timeIntroTittle);
    }

    protected virtual IEnumerator CountDownMission()
    {
        this.ChangeState(MissionState.CountdownStage);
        float currentReady = this.timeReadyMission;
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

        float currentMission = this.timeMission;
        while (currentMission > 0 && this.currentState == MissionState.ActiveGameplay)
        {
            OnTimeTick?.Invoke(Mathf.CeilToInt(currentMission));
            yield return null;
            currentReady -= Time.deltaTime;
        }
        //if (this.currentState == MissionState.ActiveGameplay)
        //{
        //    OnTimeTick?.Invoke(0f);
        //    isMissionActive = false;
        //}
    }

    protected virtual void SuccessMission()
    {
        this.ChangeState(MissionState.Success);
        Debug.Log("Done Mission");
    }



    protected virtual void ChangeState(MissionState state)
    {
        this.currentState = state;
        UpdateStateMission?.Invoke(state);
    }
}
