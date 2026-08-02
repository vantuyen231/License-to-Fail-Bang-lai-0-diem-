using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCarBtn : ButtonAbstract
{
    protected override void OnClick()
    {
        GameManager.Instance.NextCar();
    }


}
