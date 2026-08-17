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
    [SerializeField] protected int currentScoreMission;

    [Header("Status Wanted")]
    [SerializeField] protected int star;
    [SerializeField] protected int status;

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

    public int CurrentScore => currentLicense;

    public int Star => star;

    protected override void Start()
    {
        currentLicense = maxLicense;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateGameplayData(currentLicense, star, status);
        }
    }




    public virtual void AddScore(HitObjectType type, int scoreReward, string nameHit)
    {
        Debug.Log("Type Hit car: " + type + ".Name: " + nameHit + ".Score: " + scoreReward);
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

        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateGameplayData(currentLicense, star, status);
        }
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

    protected virtual void HandlePoliceHit(int scoreReward)
    {
        star = star + 1;
    }


    protected virtual void StatusPlayer()
    {
        if(currentLicense > 8) status = 0;
        if(currentLicense > 4 && currentLicense <= 8) status = 1;
        if (currentLicense <= 4) status = 2;
        
    }

    public virtual void DestinationHit()
    {
        Debug.Log("Hit");
        this.ComputeEarnedCoins();


       
    }

    protected virtual void ComputeEarnedCoins()
    {
        float licenseMultiplier = (float)currentLicense / maxLicense;
        float earned = this.baseMissionReward * licenseMultiplier;

        float safeBonus = this.GetBonusRate(vehicleCollision, pedestrianCollision);
        float bonusCoins = baseMissionReward * safeBonus;


        totalSessionCoins = Mathf.RoundToInt(earned +  bonusCoins);
        Debug.Log("Earned: " + earned + ", sefaBonusPercent: " + safeBonus + ", BonusCoins: " + bonusCoins);
        sumSessionCoins = sumSessionCoins + totalSessionCoins;
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
