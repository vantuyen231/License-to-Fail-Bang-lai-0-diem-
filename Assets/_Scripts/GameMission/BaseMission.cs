using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMission : TuyenMonoBehaviour
{
    [SerializeField] protected MissionType missionType;
    [SerializeField] protected string missionName;
    [SerializeField] protected string missionDescription;

    [SerializeField] protected bool isComplete = false;

    protected virtual void StartMission()
    {
        this.isComplete = false;
        gameObject.SetActive(true);
    }

    protected virtual void FinishMission()
    {
        this.isComplete = true;
        gameObject.SetActive(false);
    }
}
