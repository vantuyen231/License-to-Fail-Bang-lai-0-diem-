using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpTutorialBtn : ButtonAbstract
{
    protected virtual void OpenTutorial()
    {
        TutorialUI.Instance.Show();
    }

    protected virtual void CloseMenu()
    {
        MenuUI.Instance.Hide();
    }
    protected override void OnClick()
    {
        this.OpenTutorial();
        this.CloseMenu();
    }
}
