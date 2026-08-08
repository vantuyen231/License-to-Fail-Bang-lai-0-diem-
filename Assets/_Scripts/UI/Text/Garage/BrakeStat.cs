using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeStat : SliderShopAbstrast
{
    protected override void UpdateCarStats()
    {
        base.UpdateCarStats();
        this.slider.value = ShopManager.Instance.CarStatsTest.brakeForce;
    }
}
