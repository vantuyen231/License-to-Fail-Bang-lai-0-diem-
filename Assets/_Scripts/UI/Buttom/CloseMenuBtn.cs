using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenuBtn : ButtonAbstract
{


    public virtual void CloseMenu()
    {
        MenuUI.Instance.Hide();
    }
    protected override void OnClick()
    {
        this.CloseMenu();
    }
}
