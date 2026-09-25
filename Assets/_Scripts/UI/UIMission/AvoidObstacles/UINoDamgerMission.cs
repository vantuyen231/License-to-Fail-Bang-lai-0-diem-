using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class UINoDamgerMission : BaseUIMission
{
    [SerializeField] protected int timeEndUI = 2;


    protected override void Start()
    {
        base.Start();
        this.Hide(titleNoDMission.transform);
        //this.Hide(numEndMission.transform);

    }


    #region LoandComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNumEndMission();
    }

    protected virtual void LoadNumEndMission()
    {
        if (numEndMission != null) return;
        numEndMission = GetComponentInChildren<CountDownMission>();
        Debug.Log(transform.name + " LoadNumCountDown:", gameObject);
    }
    #endregion
}
