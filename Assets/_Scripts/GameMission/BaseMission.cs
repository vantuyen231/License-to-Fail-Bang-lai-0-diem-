using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMission : TuyenMonoBehaviour
{
    [SerializeField] protected MissionManager missionManager;

    [Header("Mission:")]
    [SerializeField] protected MissionType missionType = MissionType.None;
    [SerializeField] protected string missionName;
    [SerializeField] protected string missionDescription;
    [SerializeField] protected int scoreMission = 100;

    [Header("Status Mission")]
    [SerializeField] protected float timeIntroTittle = 3f;
    [SerializeField] protected float timeReadyMission = 3f;
    [SerializeField] protected float currentReady = 0f;
    [SerializeField] protected float timeMissionBase = 0f;
    [SerializeField] protected int endUIMission = 2;

    [Header("Mission States")]
    [SerializeField] protected bool isMissionActive = false;
    [SerializeField] protected MissionState currentState = MissionState.None;
    [SerializeField] protected bool isComplete = false;
    [SerializeField] protected bool isSucess = false;


    public static event Action<MissionType,MissionState,float> UpdateStateMission;
    public static event Action<float> OnTimeTick;
    public static event Action<int> GetScoreMission;

    public MissionType MissionType => missionType;
    public MissionState CurrentState => currentState;

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

    protected virtual void ChangeState(MissionState state, float duration)
    {
        Debug.Log(state);
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
        this.isMissionActive = true;
        this.ChangeState(MissionState.ActiveGameplay, timeMissionBase);
    }

    protected virtual void FinishMission(bool state)
    {
        if (isComplete) return;
        this.isComplete = true;
        isMissionActive = false;
        if (state == true)
        {
            ChangeState(MissionState.Success, endUIMission);
            GetScoreMission?.Invoke(scoreMission);
        }else
        {
            ChangeState(MissionState.Failed, endUIMission);
        }
        StartCoroutine(CountDownEndUI(state));

    }

    protected virtual IEnumerator CountDownEndUI(bool state)
    {
        yield return new WaitForSeconds(endUIMission);
        if (missionManager != null)
        {
            missionManager.CheckDoneMission(this, state);
        }

        gameObject.SetActive(false);
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
