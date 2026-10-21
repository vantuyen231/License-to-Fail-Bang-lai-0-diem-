using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostCar : TextShopAbstrast
{
    public void UpdateCost(int cost)
    {
        if (this.textMeshProUGUI == null) return;
        textMeshProUGUI.text = "$ " + cost.ToString();
    }
}
