using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCarBtn : ButtonAbstract
{
    protected override void OnClick()
    {
        base.OnClick();
        //AudioManager.Instance.SoundSFX(clickBtn);
        ShopManager.Instance.NextCar();
    }


}
