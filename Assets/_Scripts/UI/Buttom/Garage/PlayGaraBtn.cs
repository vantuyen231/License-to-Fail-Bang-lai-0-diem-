using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGaraBtn : ButtonAbstract
{
    [SerializeField] protected int nextScene = 2;
    protected override void OnClick()
    {
        if(MenuUI.Instance.isShow == true) return;
        base.OnClick();
        this.PlayGame();
    }

    protected virtual void PlayGame()
    {
        GameManager.Instance.GetRandomNumMission();
        SceneManager.LoadScene(this.nextScene);
    }
}
