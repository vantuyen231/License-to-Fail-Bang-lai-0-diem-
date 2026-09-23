using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMiddle : TuyenMonoBehaviour
{
    [SerializeField] protected UINoDamgerMission noDamgerMission;
    [SerializeField] protected UIEscapePoliceMission escapePoliceMission;
    [SerializeField] protected CompleteMission completeMission;
    [SerializeField] protected FailMisssion failMission;

    protected override void Start()
    {
        //this.Hide(completeMission.transform);
        //this.Hide(failMission.transform);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCompleteMission();
        this.LoadFailMisssion();
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
}
