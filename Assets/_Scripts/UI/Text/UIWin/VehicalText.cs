using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicalText : TextAbstract
{
    public virtual void UpdateBonus(string numVehical)
    {
        textMeshProUGUI.text = ("Bonus: " + numVehical);
    }
}
