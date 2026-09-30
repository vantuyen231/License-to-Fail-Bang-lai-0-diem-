using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrialText : TextAbstract
{
    public virtual void UpdatePedestrial(int numPedes)
    {
        textMeshProUGUI.text = ("Pedestrial: " + numPedes);
    }
}
