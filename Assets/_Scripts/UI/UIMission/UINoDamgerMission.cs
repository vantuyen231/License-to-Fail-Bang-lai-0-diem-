using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINoDamgerMission : TuyenMonoBehaviour
{
    [SerializeField] protected NumCountDown countDown;
    protected virtual void OnEnable()
    {
        MissionAvoidObstacles.OnUpdateCountdown += HandleUpdateCountdown;
    }

    protected virtual void OnDisable()
    {
        MissionAvoidObstacles.OnUpdateCountdown -= HandleUpdateCountdown;
    }

    protected virtual void HandleUpdateCountdown(int countdown)
    {
        countDown.UpdateCD(countdown);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNumCD();
    }

    protected virtual void LoadNumCD()
    {
        if(countDown != null) return;
        countDown = GetComponentInChildren<NumCountDown>();
        Debug.Log(transform.name + " LoadNumCD:",gameObject);
    }
}
