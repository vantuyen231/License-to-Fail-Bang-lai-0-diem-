using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionAvoidObstacles : BaseMission
{
    [Header("Mission Configs")]
    [SerializeField] protected float timeMission = 30f;
    [SerializeField] protected int scoreMission = 100;
    [SerializeField] protected float secondsLeft = 0;
    [SerializeField] protected UINoDamgerMission uiMission;

    private Coroutine timerCoroutine;

    protected virtual void OnEnable()
    {
        PlayerScore.OnPlayerHit += HitDetection;
    }

    protected virtual void OnDisable()
    {
        PlayerScore.OnPlayerHit -= HitDetection;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIMission();
    }

    protected virtual void LoadUIMission()
    {
        if (this.uiMission != null) return;

        this.uiMission = FindObjectOfType<UINoDamgerMission>();
        Debug.Log(transform.name + ": LoadUIMission", gameObject);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.missionType = MissionType.NoViolation;
        this.missionName = "Avoid Obstacles";
        this.missionDescription = "Reach the destination without hitting any obstacles.";
    }

    protected virtual void HitDetection()
    {
        if (this.currentState != MissionState.ActiveGameplay) return;
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        Debug.Log("Mision AO: mission fail");
        this.FinishMission(false);
    }


    protected virtual IEnumerator GameplayTimerRoutine()
    {
        secondsLeft = this.timeMission;
        while (secondsLeft > 0 && this.currentState == MissionState.ActiveGameplay)
        {

            SendTime(Mathf.Max(0f, this.secondsLeft));
            yield return null;
            secondsLeft -= Time.deltaTime;
        }
        if (this.currentState == MissionState.ActiveGameplay)
        {
            SendTime(0.0f);
            this.FinishMission(true);

        }
    }

    protected override void StartMission()
    {
        if (uiMission != null) uiMission.gameObject.SetActive(true);
        base.StartMission();
    }

    protected override IEnumerator CountDownEndUI(bool state)
    {
        yield return new WaitForSeconds(this.endUIMission);

        if (uiMission != null)
        {
            uiMission.gameObject.SetActive(false);
        }

        if (missionManager != null)
        {
            missionManager.CheckDoneMission(this, state);
        }

        gameObject.SetActive(false);
    }

    protected override void OnStartMission()
    {
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        timerCoroutine = StartCoroutine(this.GameplayTimerRoutine());
    }
}
