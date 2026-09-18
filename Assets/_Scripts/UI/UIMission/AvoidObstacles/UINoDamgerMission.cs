using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class UINoDamgerMission : TuyenMonoBehaviour
{
    [SerializeField] protected TitleNoDMission titleNoDMission;
    [SerializeField] protected CountDownMission numEndMission;
    [SerializeField] protected CompleteMission completeMission;
    [SerializeField] protected FailMisssion failMission;
    [SerializeField] protected int timeEndUI = 2;

    [Header("StateMission")]
    [SerializeField] protected MissionState stateMission = MissionState.None;


    protected override void Start()
    {
        base.Start();
        this.Hide(titleNoDMission.transform);
        this.Hide(numEndMission.transform);
    }
    protected virtual void OnEnable()
    {
        MissionAvoidObstacles.UpdateStateMission += ShowUI;
        MissionAvoidObstacles.OnTimeTick += UpdateTimerTrick;
    }

    protected virtual void OnDisable()
    {
        MissionAvoidObstacles.UpdateStateMission -= ShowUI;
        MissionAvoidObstacles.OnTimeTick -= UpdateTimerTrick;

    }


    protected virtual void ShowUI(MissionState state)
    {
        stateMission = state;
        switch (state)
        {
            case MissionState.TitleStage:
                this.Show(titleNoDMission.transform);
                this.titleNoDMission.CountDown.Hide();
                this.Hide(completeMission.transform);
                this.Hide(failMission.transform);
                //Debug.Log("Title");
                break;
            case MissionState.CountdownStage:
                titleNoDMission.CountDown.Show();
                //Debug.Log("Count Down");
                break;
            case MissionState.ActiveGameplay:
                this.Hide(titleNoDMission.transform);
                this.numEndMission.Show();
                //Debug.Log("PlayMission");
                break;
            case MissionState.Success:
                this.numEndMission.Hide();
                this.Show(completeMission.transform);
                StartCoroutine(this.CountDownUIShow(completeMission.transform));
                //Debug.Log("Mission Complete");
                break;
            case MissionState.Failed:
                this.numEndMission.Hide();
                this.Show(failMission.transform);
                StartCoroutine(CountDownUIShow(failMission.transform));
                //Debug.Log("Mission Fail");
                break;
            default:
                Debug.Log("Null");
                break;
        }
    }


    protected virtual void UpdateTimerTrick(float time)
    {
        switch (this.stateMission)
        {
            case MissionState.CountdownStage:
                titleNoDMission.CountDown.UpdateCD(Mathf.CeilToInt(time));
                break;
            case MissionState.ActiveGameplay:
                numEndMission.UpdateCDEndMission(time);
                break;
            default:
                Debug.Log("Null");
                break;
        }
    }

    protected virtual IEnumerator CountDownUIShow(Transform uiCheckTime)
    {
        yield return new WaitForSeconds(timeEndUI);
        this.Hide(uiCheckTime);
    }

    #region LoandComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTitleMission();
        this.LoadNumEndMission();
        this.LoadCompleteMission();
        this.LoadFailMisssion();
    }

    protected virtual void LoadTitleMission()
    {
        if (titleNoDMission != null) return;
        titleNoDMission = GetComponentInChildren<TitleNoDMission>();
        Debug.Log(transform.name + " LoadTitleMission:", gameObject);
    }

    protected virtual void LoadNumEndMission()
    {
        if (numEndMission != null) return;
        numEndMission = GetComponentInChildren<CountDownMission>();
        Debug.Log(transform.name + " LoadNumCountDown:", gameObject);
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
