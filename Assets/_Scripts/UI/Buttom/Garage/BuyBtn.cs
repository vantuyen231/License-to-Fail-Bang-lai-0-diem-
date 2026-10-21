using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyBtn : ButtonAbstract
{
    protected override void OnClick()
    {
        if (button != null && (!button.interactable || !button.enabled)) return;

        if (ShopManager.Instance != null && ShopManager.Instance.CarStatsTest != null)
        {
            this.BuyCar();
        }
    }

    protected virtual void BuyCar()
    {
        ShopManager.Instance.BuyCar();
    }
}
