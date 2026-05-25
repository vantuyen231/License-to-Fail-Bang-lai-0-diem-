using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCtrl : TuyenMonoBehaviour
{
    [SerializeField] protected HomeCtrl homeCtrl;

    protected override void Awake()
    {
        base.Awake();
        this.OffArea();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHomeCtrl();
    }

    protected virtual void LoadHomeCtrl()
    {
        if(homeCtrl != null) return;
        homeCtrl = GetComponentInChildren<HomeCtrl>();
    }

    public virtual void OnArea()
    {
        this.SetBuildingsActive(true);
    }

    public virtual void OffArea()
    {
        this.SetBuildingsActive(false);
    }
    public void SetBuildingsActive(bool isAreaActive)
    {
        if(homeCtrl.gameObject != null)
        {
            homeCtrl.gameObject.SetActive(isAreaActive);
        }
    }
}
