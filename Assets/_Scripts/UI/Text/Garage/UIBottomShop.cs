using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBottomShop : TuyenMonoBehaviour
{
    [SerializeField] protected CostCar costUI;
    [SerializeField] protected BuyBtn buyUI;
    public bool show = true;

    protected virtual void FixedUpdate()
    {
        this.UpdateDisplayCost();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCostCar();
        this.LoadBuyBtn();
    }

    protected virtual void LoadCostCar()
    {
        if (costUI != null) return;
        costUI = GetComponentInChildren<CostCar>();
        Debug.Log(transform.name + ": LoadCostCar", gameObject);
    }

    protected virtual void LoadBuyBtn()
    {
        if (buyUI != null) return;
        buyUI = GetComponentInChildren<BuyBtn>();
        Debug.Log(transform.name + ": LoadBuyBtn", gameObject);
    }

    protected virtual void UpdateDisplayCost()
    {
        if(show == false)
        {
            buyUI.Hide();
        }
        if(show == true) buyUI.Show();
    }
}
