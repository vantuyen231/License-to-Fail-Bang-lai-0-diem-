using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGaraBtn : ButtonAbstract
{
    [SerializeField] protected int nextScene = 2;
    protected override void OnClick()
    {
        this.PlayGame();
    }

    protected virtual void PlayGame()
    {
        SceneManager.LoadScene(this.nextScene);
    }
}
