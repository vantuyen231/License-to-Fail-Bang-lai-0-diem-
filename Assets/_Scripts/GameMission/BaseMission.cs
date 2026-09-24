using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMission : TuyenMonoBehaviour
{
    [SerializeField] protected MissionType missionType;
    [SerializeField] protected MissionManager missionManager;
    [Header("Status Mission")]
    [SerializeField] protected string missionName;
    [SerializeField] protected string missionDescription;

    [SerializeField] protected bool isComplete = false;
    [SerializeField] protected bool isSucess = false;

    [SerializeField] protected int endUIMission = 2;

    //--------------------------------------
    [SerializeField] protected float timeIntroTittle = 3f;
    [SerializeField] protected float timeReadyMission = 3f;
    [SerializeField] protected float currentReady = 0f;
    [SerializeField] protected float timeMissionBase = 0f;


    [Header("Mission States")]
    [SerializeField] protected bool isMissionActive = false;
    [SerializeField] protected MissionState currentState = MissionState.None;


    public static event Action<MissionType,MissionState,float> UpdateStateMission;
    public static event Action<float> OnTimeTick;


    protected override void Start()
    {
        base.Start();
        this.StartMission();
    }

    protected virtual void StartMission()
    {
        this.isMissionActive = false;
        this.isComplete = false;
        gameObject.SetActive(true);
        StartCoroutine(this.MissionFlowRoutine());
    }

    protected virtual IEnumerator MissionFlowRoutine()
    {
        yield return StartCoroutine(this.TitleMission());
        yield return StartCoroutine(this.CountDownMission());
        this.ActiveMission();
        this.OnStartMission();
    }

    protected abstract void OnStartMission();

    protected virtual IEnumerator CountDownEndUI(bool state)
    {
        yield return new WaitForSeconds(endUIMission);
        if (missionManager != null)
        {
            missionManager.CheckDoneMission(this, state);
        }

        gameObject.SetActive(false);
    }

    protected virtual void ChangeState(MissionState state, float duration)
    {
        this.currentState = state;
        UpdateStateMission?.Invoke(missionType,state,duration);
    }

    protected virtual IEnumerator TitleMission()
    {
        this.ChangeState(MissionState.TitleStage, timeIntroTittle);
        yield return new WaitForSeconds(timeIntroTittle);
    }

    protected virtual IEnumerator CountDownMission()
    {
        this.ChangeState(MissionState.CountdownStage, timeReadyMission);
        currentReady = this.timeReadyMission;
        while (currentReady > 0)
        {
            OnTimeTick?.Invoke(Mathf.CeilToInt(currentReady));
            yield return new WaitForSeconds(1f);
            currentReady -= 1f;
        }
        currentReady = 0;
    }

    protected virtual void ActiveMission()
    {
        this.ChangeState(MissionState.ActiveGameplay, timeMissionBase);
        this.isMissionActive = true;
    }

    protected virtual void FinishMission(bool state)
    {
        this.isComplete = true;
        isMissionActive = false;
        this.ChangeState(state ? MissionState.Success : MissionState.Failed, endUIMission);
        StartCoroutine(CountDownEndUI(state));

    }


    protected virtual void SendTime(float timer)
    {
        OnTimeTick?.Invoke(timer);
    }

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMissionManager();
    }

    protected virtual void LoadMissionManager()
    {
        if (missionManager != null) return;
        missionManager = GetComponentInParent<MissionManager>();
        Debug.Log(transform.name + ": LoadMissionManager", gameObject);
    }
    #endregion
}
