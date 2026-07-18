using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TuyenSingleton<GameManager>
{
    [SerializeField] protected int currentScore = 12;
    [SerializeField] protected int currentStars = 0;
    [SerializeField] protected int currentStatus = 0;
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
}