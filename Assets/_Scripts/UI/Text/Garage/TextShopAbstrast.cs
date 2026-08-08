using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TextShopAbstrast : TextAbstract
{
    protected override void Start()
    {
        base.Start();
        this.OnCarChangedHandler();
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
    }
}
