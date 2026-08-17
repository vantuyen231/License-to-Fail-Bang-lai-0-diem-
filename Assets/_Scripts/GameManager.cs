using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TuyenSingleton<GameManager>
{
    [Header("Player Status")]
    [SerializeField] protected int currentScore = 12;
    [SerializeField] protected int scoreMission = 0;
    [SerializeField] protected int currentStars = 0;
    [SerializeField] protected int currentStatus = 0;
    [SerializeField] private int currentVelocity =0;
    [SerializeField] protected int coin = 0;

    [SerializeField] protected bool isPauseGame = false;

    [SerializeField] protected CarPlayerDataSO carPlayerData;

    public int CurrentScore => currentScore;
    public int ScoreMission => scoreMission;
    public int CurrentStars => currentStars;
    public int CurrentStatus => currentStatus;
    public int CurrentVelocity => currentVelocity;

    public CarPlayerDataSO CarPlayerData => carPlayerData;



    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateGameplayData(int score, int stars, int status)
    {
        this.currentScore = score;
        this.currentStars = stars;
        this.currentStatus = status;
    }

    public void UpdateVelocity(int velocity)
    {
        this.currentVelocity = velocity;
    }

    public void GetUseCar(CarPlayerDataSO car)
    {
        this.carPlayerData = car;
    }

    public void UpdateScorePlayer(int scorePlyer)
    {
        this.scoreMission = scorePlyer;
    }

    protected void CoinPlayer()
    {

    }

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
}