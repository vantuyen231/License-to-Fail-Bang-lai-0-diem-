using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinText : TextAbstract
{
    public virtual void UpdateBonus(string numCoin)
    {
        textMeshProUGUI.text = ("Coin: " + numCoin);
    }
}
