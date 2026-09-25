using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIMiddle : TuyenMonoBehaviour
{
    [SerializeField] protected UINoDamgerMission noDamgerMission;
    [SerializeField] protected UIEscapePoliceMission escapePoliceMission;
    [SerializeField] protected CompleteMission completeMission;
    [SerializeField] protected FailMisssion failMission;
    [SerializeField] protected CountDownMission countDownMission;

    [SerializeField] protected MissionType missionActive = MissionType.None;
    [SerializeField] protected BaseUIMission currentUIMission;

    protected override void Start()
    {
        this.HideAllMission();
    }

    protected virtual void OnEnable()
    {
        BaseMission.UpdateStateMission += ShowUIMission;
        BaseMission.OnTimeTick += ShowCountDown;
    }


    protected virtual void OnDisable()
    {
        BaseMission.UpdateStateMission -= ShowUIMission;
        BaseMission.OnTimeTick -= ShowCountDown;

    }

    protected void ShowUIMission(MissionType type, MissionState state, float dur)
    {
        missionActive = type;
        if (state == MissionState.TitleStage)
        {
            currentUIMission = GetCurrentMission(type);
        }
        this.ActiveUIMission(type);
        this.ActiveEndMission(state,dur);
    }

    protected virtual void ActiveUIMission(MissionType missionType)
    {
        this.HideAllMission();

        if(currentUIMission == null) return;
        this.Show(currentUIMission.transform);
    }

    protected virtual void ActiveEndMission(MissionState state, float duration)
    {
        //this.HideAllMission();
        switch (state)
        {
            case MissionState.Success:
                Show(completeMission.transform);
                break;
            case MissionState.Failed:
                Show(failMission.transform); break;
            default:
                break;
        }
    }

    private void ShowCountDown(float obj)
    {
        
    }

    protected virtual BaseUIMission GetCurrentMission(MissionType missionType)
    {
        switch (missionType)
        {
            case MissionType.ReachLocation:
                return noDamgerMission;
            case MissionType.EscapePolice:
                return escapePoliceMission;
            case MissionType.NoViolation:
            case MissionType.MaintainSpeed:
            default:
                return null;
        }
    }

    protected virtual void HideAllMission()
    {
        this.Hide(noDamgerMission.transform);
        this.Hide(escapePoliceMission.transform);
        this.Hide(completeMission.transform);
        this.Hide(failMission.transform);
        this.Hide(countDownMission.transform);
    }
    #region LoadComponent
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCompleteMission();
        this.LoadFailMisssion();
        this.LoadNoDamagerMission();
        this.LoadEscapeMission();
        this.LoadCountDownMission();
    }

    private void LoadNoDamagerMission()
    {
        if (noDamgerMission != null) return;
        noDamgerMission = GetComponentInChildren<UINoDamgerMission>();
        Debug.Log(transform.name + " LoadNoDamagerMission:", gameObject);
    }

    private void LoadEscapeMission()
    {
        if (escapePoliceMission != null) return;
        escapePoliceMission = GetComponentInChildren<UIEscapePoliceMission>();
        Debug.Log(transform.name + " LoadEscapeMission:", gameObject);
    }

    protected virtual void LoadCompleteMission()
    {
        if (completeMission != null) return;
        completeMission = GetComponentInChildren<CompleteMission>();
        Debug.Log(transform.name + " LoadCompleteMission:", gameObject);
    }

    protected virtual void LoadFailMisssion()
    {
        if (failMission != null) return;
        failMission = GetComponentInChildren<FailMisssion>();
        Debug.Log(transform.name + " LoadFailMisssion:", gameObject);
    }

    protected virtual void LoadCountDownMission()
    {
        if (countDownMission != null) return;
        countDownMission = GetComponentInChildren<CountDownMission>();
        Debug.Log(transform.name + " LoadCountDownMission:", gameObject);
    }

    #endregion

    #region Show/Hide UI
    public virtual void Show(Transform uiShow)
    {
        uiShow.gameObject.SetActive(true);
    }

    public virtual void Hide(Transform uiShow)
    {
        uiShow.gameObject.SetActive(false);
    }
    #endregion
}
