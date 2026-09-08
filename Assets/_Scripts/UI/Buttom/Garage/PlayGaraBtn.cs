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
        GameManager.Instance.GetRandomNumMission();
        SceneManager.LoadScene(this.nextScene);
        ShopManager.Instance.SetUseCar();
    }
}
