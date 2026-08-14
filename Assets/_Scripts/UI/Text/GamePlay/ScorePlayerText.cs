using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePlayerText : TextAbstract
{
    public virtual void UpdateScore(string score)
    {
        textMeshProUGUI.text = ("Score Mission: "+ score);
    }
}
