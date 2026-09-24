using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMiddle : TuyenMonoBehaviour
{
    [SerializeField] protected UINoDamgerMission noDamgerMission;
    [SerializeField] protected UIEscapePoliceMission escapePoliceMission;
    [SerializeField] protected CompleteMission completeMission;
    [SerializeField] protected FailMisssion failMission;

    [SerializeField] protected List<BaseUIMission> missions;
    [SerializeField] protected MissionType missionActive = MissionType.None;

    protected override void Start()
    {
        this.Hide(completeMission.transform);
        this.Hide(failMission.transform);
    }

    protected virtual void OnEnable()
    {
        BaseMission.UpdateStateMission += ShowUIMission;
    }

    protected virtual void OnDisable()
    {
        BaseMission.UpdateStateMission -= ShowUIMission;
    }

    protected void ShowUIMission(MissionType type, MissionState state, float dur)
    {
        this.ActiveUIMission(type);
    }

    protected virtual void ActiveUIMission(MissionType missionType)
    {

    }
    #region LoadComponent
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCompleteMission();
        this.LoadFailMisssion();
        this.LoadNoDamagerMission();
        this.LoadEscapeMission();
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
