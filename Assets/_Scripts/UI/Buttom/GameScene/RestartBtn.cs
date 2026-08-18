using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartBtn : ButtonAbstract
{
    protected override void OnClick()
    {
        this.RestartGame();
    }

    protected virtual void RestartGame()
    {
        GameManager.Instance.ContinueGame();
        int indexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indexScene);
        Debug.Log("Restart");
    }
}
