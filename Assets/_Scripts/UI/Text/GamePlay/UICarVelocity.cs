using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICarVelocity : TextAbstract
{


    public virtual void SetCarVelocity(string velocityCar)
    {
        textMeshProUGUI.text = velocityCar;
    }
}
