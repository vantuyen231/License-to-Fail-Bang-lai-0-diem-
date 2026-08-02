using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TuyenSingleton<GameManager>
{
    [Header("Player Status")]
    [SerializeField] protected int currentScore = 12;
    [SerializeField] protected int currentStars = 0;
    [SerializeField] protected int currentStatus = 0;
    [SerializeField] private int currentVelocity =0;

    public int CurrentScore => currentScore;
    public int CurrentStars => currentStars;
    public int CurrentStatus => currentStatus;
    public int CurrentVelocity => currentVelocity;


    [Header("ShopCar")]
    [SerializeField] protected int currentCar = 0;

    public int CurrentCar => currentCar;
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

    public void NextCar()
    {
        currentCar++;
        Debug.Log("nextCar");
    }
}