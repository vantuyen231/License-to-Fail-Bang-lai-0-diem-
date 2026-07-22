using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBtn : ButtonAbstract
{
    protected virtual void OpenMenu()
    {
        MenuUI.Instance.Show();
    }

    protected override void OnClick()
    {
        this.OpenMenu();
    }
}
