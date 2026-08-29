using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionText : TextAbstract
{
    public virtual void UpdateBonus(string numMission)
    {
        textMeshProUGUI.text = ("Bonus: " + numMission);
    }
}
