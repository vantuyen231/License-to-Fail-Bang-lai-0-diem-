using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopBtn : ButtonAbstract
{
    [SerializeField] protected int sceneName = 1;

    protected override void OnClick()
    {
        base.OnClick();
        this.SwitchScene();
    }

    protected virtual void SwitchScene()
    {
        GameManager.Instance.ContinueGame();

        SceneManager.LoadScene(this.sceneName);
    }
}
