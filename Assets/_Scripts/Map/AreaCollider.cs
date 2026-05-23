using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollider : TuyenMonoBehaviour
{
    protected CityCtrl cityCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCityCtrl();
    }

    protected virtual void LoadCityCtrl()
    {
        this.cityCtrl = GetComponentInParent<CityCtrl>();
    }
    public void TriggerMapEnter()
    {
        if(this.cityCtrl != null)
        {
            cityCtrl.OnArea();
        }
        Debug.Log("Trigger Map: on");
    }
    public void TriggerMapExit()
    {
        if (this.cityCtrl != null)
        {
            cityCtrl.OffArea();
        }
        Debug.Log("Trigger Map: off");
    }
}
