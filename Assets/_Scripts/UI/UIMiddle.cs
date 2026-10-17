using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIMiddle : TuyenMonoBehaviour
{
    [SerializeField] protected UINoDamgerMission noDamgerMission;
    [SerializeField] protected UIEscapePoliceMission escapePoliceMission;
    [SerializeField] protected UIGoToPointMission toPointMission;
    [SerializeField] protected CompleteMission completeMission;
    [SerializeField] protected FailMisssion failMission;
    [SerializeField] protected CountDownMission countDownMission;
    [SerializeField] protected UISpeedDeliveryMission speedDeliveryMission;
    [SerializeField] protected UINDestinationMission multiDestinationMission;

    [SerializeField] protected MissionType missionActive = MissionType.None;
    [SerializeField] protected MissionState missionState = MissionState.None;
    [SerializeField] protected BaseUIMission currentUIMission;

    private Coroutine endUICoroutine;
    protected override void Start()
    {
        this.HideAllMission();
    }

    protected virtual void OnEnable()
    {
        BaseMission.UpdateStateMission += ShowUIMission;
        BaseMission.OnTimeTick += ShowCountDown;
        BaseMission.GetScoreMission += ShowUIScore;
        PlayerScore.CallScorePlayer += ShowUIWinGame;
    }



    protected virtual void OnDisable()
    {
        BaseMission.UpdateStateMission -= ShowUIMission;
        BaseMission.OnTimeTick -= ShowCountDown;
        BaseMission.GetScoreMission -= ShowUIScore;
    }

    protected void ShowUIMission(MissionType type, MissionState state, float dur)
    {
        missionActive = type;
        missionState = state;
        if (state == MissionState.TitleStage)
        {
            currentUIMission = GetCurrentMission(type);
        }
        this.ActiveUIMission(type);
        this.UIMission(type, state, dur);
        this.ActiveEndMission(state, dur);

    }

    protected virtual void ActiveUIMission(MissionType missionType)
    {
        this.HideAllMission();

        if(currentUIMission == null) return;
        this.Show(currentUIMission.transform);
    }

    protected virtual void ActiveEndMission(MissionState state, float duration)
    {
        if (endUICoroutine != null)
        {
            StopCoroutine(endUICoroutine);
            endUICoroutine = null;
        }
        switch (state)
        {
            case MissionState.Success:
                Show(completeMission.transform);
                StartCoroutine(CountDownUIShow(completeMission.transform,duration));
                break;
            case MissionState.Failed:
                Show(failMission.transform);
                StartCoroutine(CountDownUIShow(failMission.transform, duration));
                break;
            default:
                break;
        }
    }

    protected void ShowCountDown(float timer)
    {
        switch (missionState)
        {
            case MissionState.CountdownStage:
                currentUIMission.UpdateTimerTrick(timer);
                break;
            case MissionState.ActiveGameplay:
                this.Show(countDownMission.transform);
                countDownMission.UpdateCDEndMission(timer);
                break;
            default :
                break;
        }

    }

    protected virtual void UIMission(MissionType type, MissionState state, float dur)
    {
        currentUIMission.ShowUI(type, state, dur);
    }

    protected void ShowUIScore(int index)
    {
        completeMission.UIScoreSuccess.UpdateScoreMission(index);
    }

    protected virtual BaseUIMission GetCurrentMission(MissionType missionType)
    {
        switch (missionType)
        {
            case MissionType.ReachLocation:
                return toPointMission;
            case MissionType.EscapePolice:
                return escapePoliceMission;
            case MissionType.NoViolation:
                return noDamgerMission;
            case MissionType.TimedDelivery:
                return speedDeliveryMission;
            case MissionType.SequentialRoute:
                return multiDestinationMission;
            default:
                return null;
        }
    }

    protected virtual void HideAllMission()
    {
        this.Hide(noDamgerMission.transform);
        this.Hide(escapePoliceMission.transform);
        this.Hide(toPointMission.transform);
        this.Hide(completeMission.transform);
        this.Hide(failMission.transform);
        this.Hide(countDownMission.transform);
        this.Hide(speedDeliveryMission.transform);
        this.Hide(multiDestinationMission.transform);
    }

    protected virtual IEnumerator CountDownUIShow(Transform uiCheckTime, float duration)
    {
        yield return new WaitForSeconds(duration);
        this.Hide(uiCheckTime);
    }

    protected virtual void ShowUIWinGame(float bonus, int score, int hitCar, int hitNPC, int coin)
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
        this.LoadUIGoToPointMission();
        this.LoadUISpeedDeliveryMission();
        this.LoadCountDownMission();
        this.LoadUINDestinationMission();
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

    private void LoadUIGoToPointMission()
    {
        if (toPointMission != null) return;
        toPointMission = GetComponentInChildren<UIGoToPointMission>();
        Debug.Log(transform.name + " LoadUIGoToPointMission:", gameObject);
    }

    private void LoadUISpeedDeliveryMission()
    {
        if (speedDeliveryMission != null) return;
        speedDeliveryMission = GetComponentInChildren<UISpeedDeliveryMission>();
        Debug.Log(transform.name + " LoadUISpeedDeliveryMission:", gameObject);
    }

    private void LoadUINDestinationMission()
    {
        if (multiDestinationMission != null) return;
        multiDestinationMission = GetComponentInChildren<UINDestinationMission>();
        Debug.Log(transform.name + " LoadUINDestinationMission:", gameObject);
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
