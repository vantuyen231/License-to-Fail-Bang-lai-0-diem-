using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : TextAbstract
{
    public virtual void UpdateBonus(string numScpre)
    {
        textMeshProUGUI.text = ("Bonus: " + numScpre);
    }
}
