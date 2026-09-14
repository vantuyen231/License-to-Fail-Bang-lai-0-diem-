using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINoDamgerMission : TuyenMonoBehaviour
{
    [SerializeField] protected TitleNoDMission titleNoDMission;
    [SerializeField] protected CountDownMission numEndMission;
    [SerializeField] protected MissionState stateMission = MissionState.None;


    protected override void Start()
    {
        base.Start();
        this.Hide(titleNoDMission.transform);
        this.Hide(numEndMission.transform);
    }
    protected virtual void OnEnable()
    {
        //MissionAvoidObstacles.SetUpMission += ShowUIMission;
        //MissionAvoidObstacles.ShowNumStage += ShowNumStart;
        //MissionAvoidObstacles.OnUpdateCountdown += HandleUpdateCountdown;
        //MissionAvoidObstacles.OnStartMission += HandMissionGo;
        //MissionAvoidObstacles.UpdateCountDownMission += CountDownEndMission;

        MissionAvoidObstacles.UpdateStateMission += ShowUI;
        MissionAvoidObstacles.OnTimeTick += UpdateTimerTrick;
    }

    protected virtual void OnDisable()
    {
        //MissionAvoidObstacles.SetUpMission -= ShowUIMission;
        //MissionAvoidObstacles.ShowNumStage -= ShowNumStart;
        //MissionAvoidObstacles.OnUpdateCountdown -= HandleUpdateCountdown;
        //MissionAvoidObstacles.OnStartMission -= HandMissionGo;
        //MissionAvoidObstacles.UpdateCountDownMission -= CountDownEndMission;

        MissionAvoidObstacles.UpdateStateMission -= ShowUI;
        MissionAvoidObstacles.OnTimeTick -= UpdateTimerTrick;

    }

    protected virtual void ShowUIMission()
    {
        this.Show(titleNoDMission.transform);
        this.titleNoDMission.CountDown.Hide();
    }

    protected virtual void HandleUpdateCountdown(int countdown)
    {
        titleNoDMission.CountDown.UpdateCD(countdown);
    }

    protected virtual void HandMissionGo()
    {
        this.Hide(titleNoDMission.transform);
        this.numEndMission.Show();
    }

    protected virtual void ShowNumStart()
    {
        titleNoDMission.CountDown.Show();
    }

    protected virtual void CountDownEndMission(float index)
    {
        numEndMission.UpdateCDEndMission(index);
    }

    protected virtual void ShowUI(MissionState state)
    {
        stateMission = state;
        switch (state)
        {
            case MissionState.TitleStage:
                this.Show(titleNoDMission.transform);
                this.titleNoDMission.CountDown.Hide();
                Debug.Log("Title");
                break;
            case MissionState.CountdownStage:
                titleNoDMission.CountDown.Show();
                Debug.Log("Count Down");
                break;
            case MissionState.ActiveGameplay:
                this.Hide(titleNoDMission.transform);
                this.numEndMission.Show();
                Debug.Log("PlayMission");
                break;
            case MissionState.Success:
                Debug.Log("Mission Complete");
                break;
            case MissionState.Failed:
                Debug.Log("Mission Fail");
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

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTitleMission();
        this.LoadNumEndMission();
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

    public virtual void Show(Transform uiShow)
    {
        uiShow.gameObject.SetActive(true);
    }

    public virtual void Hide(Transform uiShow)
    {
        uiShow.gameObject.SetActive(false);
    }
}
