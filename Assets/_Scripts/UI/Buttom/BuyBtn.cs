using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyBtn : ButtonAbstract
{

    [SerializeField] protected bool isShow;
    protected override void Start()
    {
        base.Start();
        this.OnCarChangedHandler();
    }


    protected override void OnClick()
    {
        if (button != null && (!button.interactable || !button.enabled)) return;

        if (ShopManager.Instance != null && ShopManager.Instance.CarStatsTest != null)
        {
            if (ShopManager.Instance.CarStatsTest.isBuy) return;
        }
        this.BuyCar();
    }

    protected virtual void BuyCar()
    {
        Debug.Log("Buy");
    }

    public virtual void Hide()
    {
        button.interactable = false;
        button.enabled = false;
    }

    public virtual void Show()
    {
        button.interactable = true;
        button.enabled = true;
    }

    protected virtual void CheckHide()
    {
        isShow = ShopManager.Instance.CarStatsTest.isBuy;
        if (isShow)
        {
            this.Hide();
            textBtn.text = "Select";
        }
        else
        {
            this.Show();
            textBtn.text = "Buy";
        }
    }

        protected virtual void OnEnable()
    {
        ShopManager.OnCarChanged += this.OnCarChangedHandler;

    }

    protected virtual void OnDisable()
    {
        ShopManager.OnCarChanged -= this.OnCarChangedHandler;

    }

    private void OnCarChangedHandler()
    {
        if (ShopManager.Instance == null || ShopManager.Instance.CarStatsTest == null) return;
        this.UpdateCarStats();
    }

    protected virtual void UpdateCarStats()
    {
        this.CheckHide();
    }
}
