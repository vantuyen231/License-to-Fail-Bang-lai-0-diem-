using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumCountDown : TextAbstract
{

    public virtual void UpdateCD(int indexCD)
    {
        textMeshProUGUI.text = indexCD.ToString();
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
