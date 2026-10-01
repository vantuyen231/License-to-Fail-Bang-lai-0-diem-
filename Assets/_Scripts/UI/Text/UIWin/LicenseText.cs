using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LicenseText : TextAbstract
{
    public virtual void UpdateLicense(int numLicense)
    {
        textMeshProUGUI.text = ("License: " + numLicense);
    }
}
