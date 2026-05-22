using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : TuyenMonoBehaviour
{
    [SerializeField] protected List<CityCtrl> cityCtrls = new List<CityCtrl>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCityCtrl();
    }

    protected virtual void LoadCityCtrl()
    {
        if(cityCtrls.Count > 0) return;
        this.cityCtrls.AddRange(GetComponentsInChildren<CityCtrl>());
    }
}
