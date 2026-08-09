using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostCar : TextShopAbstrast
{
    [SerializeField] protected bool isShow = true;

    protected override void Start()
    {
        base.Start();

    }

    public virtual void Hide()
    {
        textMeshProUGUI.enabled = false;
    }

    public virtual void Show()
    {
        textMeshProUGUI.enabled = true;
    }
    protected override void UpdateCarStats()
    {
        if (this.textMeshProUGUI == null) return;
        int cost = ShopManager.Instance.CarStatsTest.costCar;
        textMeshProUGUI.text = "$ " + cost.ToString();
        this.CheckHide();
    }

    protected virtual void CheckHide()
    {
        isShow = ShopManager.Instance.CarStatsTest.isBuy;
        if (isShow == true) this.Hide();
        else this.Show();

    }
}
