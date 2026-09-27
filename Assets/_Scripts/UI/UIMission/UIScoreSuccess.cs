using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScoreSuccess : TextAbstract
{
    public virtual void UpdateScoreMission(int indexScoreMission)
    {
        textMeshProUGUI.text = indexScoreMission.ToString();
    }
}
