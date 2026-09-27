using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionEscapePolice : TimeBaseMission
{
    protected override void OnTimeOut()
    {
        this.FinishMission(true);
    }
}
