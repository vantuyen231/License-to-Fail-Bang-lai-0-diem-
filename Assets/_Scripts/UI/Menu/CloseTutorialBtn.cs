using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTutorialBtn : ButtonAbstract
{
    protected override void OnClick()
    {
        this.CloseTutorial();
    }
    protected virtual void CloseTutorial()
    {
        TutorialUI.Instance.Hide();
        GameManager.Instance.ContinueGame();
    }


}
