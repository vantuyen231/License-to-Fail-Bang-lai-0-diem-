using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayBtn : ButtonAbstract
{
    [SerializeField] protected int sceneName = 1;
    protected override void OnClick()
    {
        this.SwitchScene();
    }

    protected virtual void SwitchScene()
    {
        SceneManager.LoadScene(this.sceneName);
    }

}
