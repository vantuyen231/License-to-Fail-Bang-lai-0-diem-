using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPlayerText : TextShopAbstrast
{
    protected override void UpdateCarStats()
    {
        this.UpdateCoinPlayerUI();
    }
    protected void UpdateCoinPlayerUI()
    {
        if (this.textMeshProUGUI == null) return;
        int coinPlayer = GameManager.Instance.Coin;
        textMeshProUGUI.text = ("Coin: "+coinPlayer.ToString());
    }
}
