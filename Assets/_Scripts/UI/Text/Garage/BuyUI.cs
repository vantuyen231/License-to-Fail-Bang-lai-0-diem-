using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUI : TuyenMonoBehaviour
{
    [SerializeField] protected BuyBtn buyBtn;
    [SerializeField] protected CostCar costCar;

    protected virtual void OnEnable()
    {
        ShopManager.OnCarChanged += this.OnCarChangedHandler;

    }

    protected virtual void OnDisable()
    {
        ShopManager.OnCarChanged -= this.OnCarChangedHandler;

    }

    protected void OnCarChangedHandler()
    {

    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuyBtn();
        this.LoadCostCar();
    }

    protected virtual void LoadBuyBtn()
    {
        if (buyBtn != null) return;
        buyBtn = GetComponentInChildren<BuyBtn>();
        Debug.Log(transform.name + ": LoadBuyBtn", gameObject);
    }

    protected virtual void LoadCostCar()
    {
        if (costCar != null) return;
        costCar = GetComponentInChildren<CostCar>();
        Debug.Log(transform.name + ": LoadCostCar", gameObject);
    }
}
