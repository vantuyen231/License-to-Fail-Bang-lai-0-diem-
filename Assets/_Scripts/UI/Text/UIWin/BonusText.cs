using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusText : TextAbstract
{
    public virtual void UpdateBonus(int numBonus)
    {
        textMeshProUGUI.text = ("Bonus: "+numBonus);
    }
}
