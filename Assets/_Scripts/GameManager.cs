using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : TuyenSingleton<GameManager>
{
    [Header("Player Status")]
    //[SerializeField] protected int mission = 0;
    [SerializeField] protected int coin = 0;

    [Header("State Game")]
    [SerializeField] protected bool isWinGame = false;
    [SerializeField] protected bool isLoseGame = false;
    [SerializeField] protected bool isPauseGame = false;


    [Header("Mission Game")]
    [SerializeField] protected int maxMission = 5;
    [SerializeField] protected int indexMission = 0;
    

    [SerializeField] protected CarPlayerDataSO carPlayerData;

    #region (Public Value)
    public int IndexMission => indexMission;
    public CarPlayerDataSO CarPlayerData => carPlayerData;
    #endregion


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    protected override void LoadInstance()
    {
        base.LoadInstance();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject );
            Debug.Log("Do Destroy Singletion: " + gameObject);
            return;
        }
    }

    public void GetUseCar(CarPlayerDataSO car)
    {
        this.carPlayerData = car;
    }

    protected void CoinPlayer()
    {

    }

    #region (PauseGame);
    public void PauseGame()
    {
        isPauseGame = true;
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        isPauseGame = false;
        Time.timeScale = 1f;
    }
    #endregion

    public void WinGame()
    {
        Debug.Log("You Win");
        this.PauseGame();
        WinUI.Instance.Show();
    }

    public void LoseGame()
    {
        Debug.Log("You Lose");
        LoseUI.Instance.Show();
    }

    public void GetRandomNumMission()
    {
        indexMission = UnityEngine.Random.Range(1, maxMission);
    }
}