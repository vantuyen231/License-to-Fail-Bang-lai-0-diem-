using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrevCarBtn : ButtonAbstract
{

    protected override void OnClick()
    {
        base.OnClick();
        ShopManager.Instance.PrevCar();
    }
}
