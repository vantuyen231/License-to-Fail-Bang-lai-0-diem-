using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : TuyenMonoBehaviour
{
    [SerializeField] protected int maxScore = 12;
    [SerializeField] protected int currentScore;
    [SerializeField] protected int star;
    [Header("Type of collision")]
    [SerializeField] protected int pedestrianCollision = 0;
    [SerializeField] protected int vehicleCollision = 0;
        
    [SerializeField] protected int currentHitCarNPC = 0;
    [SerializeField] protected int upStarHitCar = 2;

    public int CurrentScore => currentScore;

    public int Star => star;

    protected override void Start()
    {
        currentScore = maxScore;
    }
    public virtual void LoadCroceHitNPC()
    {
        Debug.Log("Hit NPC");
    }

    public void LoadCroceHitCarNPC()
    {
        Debug.Log("Hit carNPC");
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
        if (currentScore <= 0) this.currentScore = 0;
        if (star > 5) this.star = 5;
    }

    protected virtual void HandleNPCHit(int scoreReward)
    {
        pedestrianCollision += 1;
        currentScore -= scoreReward;
        if (currentScore > 0) return;
        star = star + 1;
    }

    protected virtual void HandleCarNPCHit(int scoreReward)
    {
        vehicleCollision += 1;
        currentScore -= scoreReward;
        if (currentScore > 0) return;
        currentHitCarNPC++;
        if(currentHitCarNPC >= upStarHitCar)
        {
            star++;
            currentHitCarNPC = 0;
        }
    }

    protected virtual void HandlePoliceHit(int scoreReward)
    {
        star = star + 1;
    }

}
