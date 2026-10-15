using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDistanceDestination : TextAbstract
{
    public virtual void DistanceDestination(int index)
    {
        textMeshProUGUI.text = (index + "m");
    }
}
