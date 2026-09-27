using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIMission : TuyenMonoBehaviour
{
    [Header("StateMission")]
    [SerializeField] protected TitleNoDMission titleNoDMission;
    [SerializeField] protected MissionState stateMission = MissionState.None;
    //[SerializeField] protected CountDownMission numEndMission;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTitleMission();
    }

    protected virtual void LoadTitleMission()
    {
        if (titleNoDMission != null) return;
        titleNoDMission = GetComponentInChildren<TitleNoDMission>();
        Debug.Log(transform.name + " LoadTitleMission:", gameObject);
    }

    public virtual void ShowUI(MissionType missionType, MissionState state, float displayDuration)
    {
        stateMission = state;
        switch (state)
        {
            case MissionState.TitleStage:
                this.Show(titleNoDMission.transform);
                this.titleNoDMission.CountDown.Hide();
                //this.Hide(completeMission.transform);

                Debug.Log("Title");
                break;
            case MissionState.CountdownStage:
                titleNoDMission.CountDown.Show();
                Debug.Log("Count Down");
                break;
            case MissionState.ActiveGameplay:
                this.Hide(titleNoDMission.transform);
                //this.numEndMission.Show();
                Debug.Log("PlayMission");
                break;
            case MissionState.Success:
                //this.numEndMission.Hide();
                //this.Show(completeMission.transform);
                //StartCoroutine(this.CountDownUIShow(completeMission.transform,displayDuration));
                Debug.Log("Mission Complete");
                break;
            case MissionState.Failed:
                //this.numEndMission.Hide();
                //StartCoroutine(CountDownUIShow(failMission.transform,displayDuration));
                Debug.Log("Mission Fail");
                break;
            default:
                Debug.Log("Null");
                break;
        }
    }

    public virtual void UpdateTimerTrick(float time)
    {
        if (stateMission == MissionState.CountdownStage && titleNoDMission != null && titleNoDMission.CountDown != null)
            titleNoDMission.CountDown.UpdateCD(Mathf.CeilToInt(time));

    }

    protected virtual IEnumerator CountDownUIShow(Transform uiCheckTime, float duration)
    {
        yield return new WaitForSeconds(duration);
        this.Hide(uiCheckTime);
    }

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
