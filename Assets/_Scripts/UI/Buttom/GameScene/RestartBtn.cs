using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartBtn : ButtonAbstract
{
    protected override void OnClick()
    {
    }

    protected virtual void RestartGame()
    {
        Debug.Log("Restart");
    }
}
