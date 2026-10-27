using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBtn : ButtonAbstract
{
    protected override void OnClick()
    {
        base.OnClick();
        this.OpenMenu();
    }
    protected virtual void OpenMenu()
    {
        MenuUI.Instance.Show();
        GameManager.Instance.PauseGame();
    }


}
