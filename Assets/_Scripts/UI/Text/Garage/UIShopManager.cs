using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopManager : TuyenMonoBehaviour
{
    protected override void Start()
    {
        transform.root.GetComponentInChildren<MenuUI>(true).LoadUpVolume();
    }



}
