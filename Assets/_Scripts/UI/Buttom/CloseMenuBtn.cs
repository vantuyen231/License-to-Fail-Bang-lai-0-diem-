using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenuBtn : ButtonAbstract
{
    protected override void OnClick()
    {
        this.CloseMenu();
    }

    public virtual void CloseMenu()
    {
        MenuUI.Instance.Hide();
        GameManager.Instance.ContinueGame();
    }

}
