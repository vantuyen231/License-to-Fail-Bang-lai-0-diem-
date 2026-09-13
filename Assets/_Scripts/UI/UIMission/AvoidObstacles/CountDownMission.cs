using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownMission : TextAbstract
{
    public virtual void UpdateCDEndMission(float currentNum)
    {
        textMeshProUGUI.text = currentNum.ToString();
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
