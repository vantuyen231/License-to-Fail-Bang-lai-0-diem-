using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedStat : SliderShopAbstrast
{
    protected override void UpdateCarStats()
    {
        base.UpdateCarStats();

        this.slider.value = ShopManager.Instance.CarStatsTest.motorForce;
    }
}
