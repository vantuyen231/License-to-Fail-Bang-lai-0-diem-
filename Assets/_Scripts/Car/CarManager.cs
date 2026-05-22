using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : TuyenMonoBehaviour
{
    [SerializeField] protected CarController controller;
    [SerializeField] protected FrontBumpCtrl frontBumpCtrl;
    [SerializeField] protected MapTrigger mapTrigger;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarCtrl();
        this.LoandFrontBumpCtrl();
        this.LoadMapTrigger();
    }

    protected virtual void LoadCarCtrl()
    {
        if(controller != null) return;
        controller = GetComponent<CarController>();
    }

    protected virtual void LoandFrontBumpCtrl()
    {
        if (frontBumpCtrl != null) return;
        frontBumpCtrl = GetComponentInChildren<FrontBumpCtrl>();
    }

    protected virtual void LoadMapTrigger()
    {
        if (mapTrigger != null) return;
        mapTrigger = GetComponentInChildren<MapTrigger>();
    }
}
