using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinText : TextAbstract
{
    public virtual void UpdateCoin(string numCoin)
    {
        textMeshProUGUI.text = ("Coin: " + numCoin);
    }
}
