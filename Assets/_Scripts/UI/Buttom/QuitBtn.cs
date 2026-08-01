using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitBtn : ButtonAbstract
{
    protected override void OnClick()
    {
        this.QuitGame();
    }

    protected virtual void QuitGame()
    {
        Debug.Log("Quit game!");

        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
