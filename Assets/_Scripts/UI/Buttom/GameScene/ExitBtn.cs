using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBtn : ButtonAbstract
{
    [SerializeField] protected int sceneName = 0;

    protected override void OnClick()
    {
        this.SwitchScene();
    }

    protected virtual void SwitchScene()
    {
        GameManager.Instance.ContinueGame();

        SceneManager.LoadScene(this.sceneName);
    }
}
