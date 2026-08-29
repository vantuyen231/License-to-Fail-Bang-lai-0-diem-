using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrialText : TextAbstract
{
    public virtual void UpdateBonus(string numPedes)
    {
        textMeshProUGUI.text = ("Bonus: " + numPedes);
    }
}
