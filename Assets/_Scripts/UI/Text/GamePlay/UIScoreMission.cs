using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScoreMission : TextAbstract
{
    public virtual void UpdateScorePlayer(int scorePlayer)
    {
        textMeshProUGUI.text = ("Score Mission: "+scorePlayer);
    }

}
