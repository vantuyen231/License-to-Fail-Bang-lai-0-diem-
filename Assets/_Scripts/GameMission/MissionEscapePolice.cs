using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionEscapePolice : TimeBaseMission
{
    [SerializeField] protected int indexSpawnPolice = 3;
    protected override void ActiveMission()
    {
        base.ActiveMission();
        PoliceSpawnCtrl.Instance.PoliceSpawning.SpawnPoliceInMission(indexSpawnPolice);
    }

    protected override void OnTimeOut()
    {
        this.FinishMission(true);
        PoliceSpawnCtrl.Instance.PoliceSpawning.ClearAllEndMission();
    }

    protected override void OnForceFail()
    {
        base.OnForceFail();
        PoliceSpawnCtrl.Instance.PoliceSpawning.ClearAllEndMission();
    }
}
