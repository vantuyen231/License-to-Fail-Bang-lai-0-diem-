using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumCountDown : TextAbstract
{

    public virtual void UpdateCD(int indexCD)
    {
        textMeshProUGUI.text = indexCD.ToString();
    }
}
