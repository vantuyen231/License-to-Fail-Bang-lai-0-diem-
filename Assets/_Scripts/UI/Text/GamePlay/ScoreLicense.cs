using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreLicense : TextAbstract
{
    public virtual void SetScoreText(string scoreStr)
    {
        textMeshProUGUI.text = scoreStr;
    }
}
