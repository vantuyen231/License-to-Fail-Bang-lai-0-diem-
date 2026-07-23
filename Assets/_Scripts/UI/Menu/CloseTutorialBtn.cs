using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTutorialBtn : ButtonAbstract
{
    protected virtual void CloseTutorial()
    {
        TutorialUI.Instance.Hide();
    }

    protected override void OnClick()
    {
        this.CloseTutorial();
    }
}
