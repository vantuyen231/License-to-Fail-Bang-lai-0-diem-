using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostCar : TextShopAbstrast
{
    protected override void UpdateCarStats()
    {
        if (this.textMeshProUGUI == null) return;
        int cost = ShopManager.Instance.CarStatsTest.costCar;
        textMeshProUGUI.text = "$ "+cost.ToString();
    }
}
