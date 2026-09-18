using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMission : TuyenMonoBehaviour
{
    [SerializeField] protected MissionType missionType;
    [SerializeField] protected MissionManager missionManager;
    [Header("Status Mission")]
    [SerializeField] protected string missionName;
    [SerializeField] protected string missionDescription;

    [SerializeField] protected bool isComplete = false;
    [SerializeField] protected bool isSucess = false;


    protected virtual void StartMission()
    {
        this.isComplete = false;
        gameObject.SetActive(true);
    }

    protected virtual void FinishMission(bool state)
    {
        this.isComplete = true;
        missionManager.CheckDoneMission(this, state);
        gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMissionManager();
    }

    protected virtual void LoadMissionManager()
    {
        if (missionManager != null) return;
        missionManager = GetComponentInParent<MissionManager>();
        Debug.Log(transform.name + ": LoadMissionManager", gameObject);
    }
}
