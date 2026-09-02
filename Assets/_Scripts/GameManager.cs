using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TuyenSingleton<GameManager>
{
    [Header("Player Status")]
    [SerializeField] protected int scoreMission = 0;
    [SerializeField] protected int currentStars = 0;
    [SerializeField] protected int currentStatus = 0;
    [SerializeField] private int currentVelocity =0;

    [Header("WinGameStatus")]
    [SerializeField] protected int mission = 0;
    [SerializeField] protected float bonus = 0;
    [SerializeField] protected int currentScore = 12;
    [SerializeField] protected int coin = 0;
    [SerializeField] protected int pedestrial = 0;
    [SerializeField] protected int vehical = 0;


    [Header("State Game")]
    [SerializeField] protected bool isWinGame = false;
    [SerializeField] protected bool isLoseGame = false;
    [SerializeField] protected bool isPauseGame = false;
    [SerializeField] protected bool isWanted = false;

    [SerializeField] protected CarPlayerDataSO carPlayerData;

    #region (Public Value)
    public int CurrentScore => currentScore;
    public int ScoreMission => scoreMission;
    public int CurrentStars => currentStars;
    public int CurrentStatus => currentStatus;
    public int CurrentVelocity => currentVelocity;
    public bool IsWanted => isWanted;

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

    #region(UI gameplay Update)
    public void UpdateGameplayData(int score, int stars, int status)
    {
        this.currentScore = score;
        this.currentStars = stars;
        this.currentStatus = status;
        Debug.Log("Add score");
    }

    public void UpdateVelocity(int velocity)
    {
        this.currentVelocity = velocity;
    }

    public void UpdateScorePlayer(int scorePlyer)
    {
        this.scoreMission = scorePlyer;
    }
    #endregion



    public void GetUseCar(CarPlayerDataSO car)
    {
        this.carPlayerData = car;
    }

    public void SetPlayerWanted(bool status)
    {
        this.isWanted = status;
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
}