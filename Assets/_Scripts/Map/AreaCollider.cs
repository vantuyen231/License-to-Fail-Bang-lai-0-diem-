using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollider : TuyenMonoBehaviour
{
    [SerializeField] protected CityCtrl cityCtrl;
    [SerializeField] protected BoxCollider areaCollider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCityCtrl();
        this.LoadAreaCollider();
    }

    protected virtual void LoadCityCtrl()
    {
        this.cityCtrl = GetComponentInParent<CityCtrl>();
    }

    protected virtual void LoadAreaCollider()
    {
        if(this.areaCollider != null) return;
        this.areaCollider = GetComponent<BoxCollider>();
    }
    public void TriggerMapEnter()
    {
        if(this.cityCtrl != null)
        {
            cityCtrl.OnArea();
        }
    }
    public void TriggerMapExit()
    {
        if (this.cityCtrl != null)
        {
            cityCtrl.OffArea();
        }
    }
}
