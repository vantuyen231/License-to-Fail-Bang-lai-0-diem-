using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameCar : TextShopAbstrast
{

    protected override void UpdateCarStats()
    {
        if (this.textMeshProUGUI == null) return;
        string name = ShopManager.Instance.CarStatsTest.nameCar;
        textMeshProUGUI.text = name;
    }
}
