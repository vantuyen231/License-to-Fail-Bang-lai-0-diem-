using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBaseMission : BaseMission
{
    [SerializeField] protected float timeMission = 30f;
    [SerializeField] protected float secondsLeft = 0;

    private Coroutine timerCoroutine;

    protected override void OnStartMission()
    {
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        timerCoroutine = StartCoroutine(this.GameplayTimerRoutine());
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
            this.OnTimeOut();
        }
    }

    protected virtual void OnTimeOut()
    {
        Debug.Log($"[{transform.name}] TIME OUT");
        this.FinishMission(false); 
    }

    protected override void FinishMission(bool state)
    {
        if (this.timerCoroutine != null)
        {
            StopCoroutine(this.timerCoroutine);
            this.timerCoroutine = null;
        }
        base.FinishMission(state);
    }

}
