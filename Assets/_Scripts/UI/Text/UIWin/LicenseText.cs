using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LicenseText : TextAbstract
{
    public virtual void UpdateBonus(string numLicense)
    {
        textMeshProUGUI.text = ("Bonus: " + numLicense);
    }
}
