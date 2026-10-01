using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionText : TextAbstract
{
    public virtual void UpdateMission(int numComplete, int maxMission)
    {
        textMeshProUGUI.text = ("Test Pass: " + numComplete + "/" + maxMission);
    }
}
