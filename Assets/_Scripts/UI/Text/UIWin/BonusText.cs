using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusText : TextAbstract
{
    public virtual void UpdateBonus(string numBonus)
    {
        textMeshProUGUI.text = ("Bonus: "+numBonus);
    }
}
