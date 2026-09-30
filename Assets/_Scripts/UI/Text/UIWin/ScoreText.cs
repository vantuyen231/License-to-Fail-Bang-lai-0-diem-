using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : TextAbstract
{
    public virtual void UpdateScore(int numScpre)
    {
        textMeshProUGUI.text = ("Score: " + numScpre);
    }
}
