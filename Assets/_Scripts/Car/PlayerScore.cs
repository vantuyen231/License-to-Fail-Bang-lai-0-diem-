using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerScore : TuyenMonoBehaviour
{
    [Header("Status License")]
    [SerializeField] protected int maxLicense = 12;
    [SerializeField] protected int currentLicense;
    [SerializeField] protected int scoreMission = 0;
    //[SerializeField] protected int currentScoreMission = 0;
    [SerializeField] protected float bonusCoins;

    [Header("Status Wanted")]
    [SerializeField] protected int star;
    [SerializeField] protected int status;
    [SerializeField] protected float starCoolDownTimer = 0;
    [SerializeField] protected float starCDMaxLimit = 20f;

    [Header("Mission Tracking")]
    [SerializeField] protected int baseMissionReward = 100;
    [SerializeField] protected int totalSessionCoins = 0;
    [SerializeField] protected int sumSessionCoins = 0;
    [SerializeField] protected int completedMissionsCount = 0;

    [Header("Type of collision")]
    [SerializeField] protected int pedestrianCollision = 0;
    [SerializeField] protected int vehicleCollision = 0;
    
    [SerializeField] protected int currentHitCar = 0;
    [SerializeField] protected int upStarHitCar = 2;

    [Header("State Game")]
    [SerializeField] protected bool isWanted = false;
    [SerializeField] protected bool isLose = false;
    [SerializeField] protected bool isWin = false;

    public int CurrentScore => currentLicense;
    public int Star => star;

    public static event Action<int, int, int, bool> ChangeStatusPlayer;
    public static event Action<HitObjectType, int, string> ShowNoti;
    public static event Action<float,int,int,int,int> CallScorePlayer;
    public static event Action OnPlayerHit;
    public static event Action<int> ScoreMissionPlayer;

    protected override void Start()
    {
        currentLicense = maxLicense;
        starCoolDownTimer = starCDMaxLimit;

        ChangeStatusPlayer?.Invoke(currentLicense, star, status, isWanted);
    }

    private void FixedUpdate()
    {
        this.CheckCoolDownStar();
    }

    protected virtual void OnEnable()
    {
        BaseMission.GetScoreMission += AddScoreMission;
        MissionManager.EndGame += AddFinalScore;
    }

    protected virtual void OnDisable()
    {
        BaseMission.GetScoreMission -= AddScoreMission;
        MissionManager.EndGame -= AddFinalScore;
    }

    protected void AddScoreMission(int scoreM)
    {
        this.scoreMission += scoreM;
        ScoreMissionPlayer?.Invoke(scoreMission);
    }

    protected virtual void AddFinalScore(bool stateGame, int missionComplete, int maxMission)
    {
        this.ComputeEarnedCoins();
        if (stateGame == true) isWin = true;
        if(stateGame == false) isLose = true;
        CallScorePlayer?.Invoke(bonusCoins, scoreMission, vehicleCollision, pedestrianCollision, sumSessionCoins);
        Debug.Log("Add final score");
    }

    public virtual void AddScore(HitObjectType type, int scoreReward, string nameHit)
    {
        OnPlayerHit?.Invoke();
        //Debug.Log("Type Hit car: " + type + ".Name: " + nameHit + ".Score: " + scoreReward);
        switch(type)
        {
            case HitObjectType.Pedestrian:
                this.HandleNPCHit(scoreReward);
                break;
            case HitObjectType.CarNPC:
                this.HandleCarNPCHit(scoreReward);
                break;
        }
        if (currentLicense <= 0) this.currentLicense = 0;
        this.StatusPlayer();
        if (star > 5) this.star = 5;

        ChangeStatusPlayer?.Invoke(currentLicense, star, status, isWanted);
        ShowNoti?.Invoke(type, scoreReward, nameHit);
    }

    protected virtual void HandleNPCHit(int scoreReward)
    {
        pedestrianCollision += 1;
        currentLicense -= scoreReward;
        if (currentLicense > 0) return;
        star = star + 1;
    }

    protected virtual void HandleCarNPCHit(int scoreReward)
    {
        vehicleCollision += 1;
        currentLicense -= scoreReward;
        if (currentLicense > 0) return;
        currentHitCar++;
        if(currentHitCar >= upStarHitCar)
        {
            star++;
            currentHitCar = 0;
        }
    }


    protected virtual void StatusPlayer()
    {
        if(currentLicense > 8) status = 0;
        if(currentLicense > 4 && currentLicense <= 8) status = 1;
        if (currentLicense <= 4) status = 2;
        
    }

    protected virtual void CheckCoolDownStar()
    {
        if (star <= 0)
        {
            isWanted = false;
            ChangeStatusPlayer?.Invoke(currentLicense, star, status, isWanted);
            return;
        }
        isWanted = true;
        ChangeStatusPlayer?.Invoke(currentLicense, star, status, isWanted);

        starCoolDownTimer -= Time.deltaTime;
        if(starCoolDownTimer >= 0) return;
        this.star--;
        ChangeStatusPlayer?.Invoke(currentLicense, star, status, isWanted);

        starCoolDownTimer = starCDMaxLimit;

    }

    public virtual void DestinationHit()
    {
        Debug.Log("Hit");
        //this.ComputeEarnedCoins();
    }

    protected virtual void ComputeEarnedCoins()
    {
        float licenseMultiplier = (float)currentLicense / maxLicense;
        float earned = this.baseMissionReward * licenseMultiplier;

        float safeBonus = this.GetBonusRate(vehicleCollision, pedestrianCollision);
        bonusCoins = baseMissionReward * safeBonus;


        totalSessionCoins = Mathf.RoundToInt(earned +  bonusCoins);
        Debug.Log("Earned: " + earned + ", sefaBonusPercent: " + safeBonus + ", BonusCoins: " + bonusCoins);
        sumSessionCoins = sumSessionCoins + totalSessionCoins;
        GameManager.Instance.CoinPlayer(sumSessionCoins);
    }

    protected virtual float GetBonusRate(int hitCar, int hitNPC)
    {

        if (hitCar <= 2  || hitNPC <= 1) return 0.50f;
        if (hitCar <= 4 && hitNPC <= 2) return 0.25f;
        if (hitCar <= 8 && hitNPC <= 2) return 0.05f;
        return 0.00f;
    }

    protected virtual void ResetStatusPlay()
    {
        pedestrianCollision = 0;
        vehicleCollision = 0;
        currentHitCar = 0;
    }

}
