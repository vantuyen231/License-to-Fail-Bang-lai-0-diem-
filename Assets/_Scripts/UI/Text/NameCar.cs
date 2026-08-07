using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameCar : TextAbstract
{
    protected virtual void FixedUpdate()
    {
        this.SetName();
    }
    public virtual void SetName()
    {
        string name = ShopManager.Instance.NameCar;
        textMeshProUGUI.text = name;
    }
}
