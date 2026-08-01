using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyBtn : ButtonAbstract
{
    protected override void OnClick()
    {
        this.BuyCar();
    }

    protected virtual void BuyCar()
    {
        Debug.Log("Buy");
    }
}
