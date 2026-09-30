using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionText : TextAbstract
{
    public virtual void UpdateMission(string numMission)
    {
        textMeshProUGUI.text = ("Bonus: " + numMission);
    }
}
