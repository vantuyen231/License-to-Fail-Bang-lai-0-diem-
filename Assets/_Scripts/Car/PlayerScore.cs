using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : TuyenMonoBehaviour
{
    [SerializeField] protected int maxScore = 12;
    [SerializeField] protected int currentScore;
    [SerializeField] protected int star;
    [Header("Type of collision")]
    [SerializeField] protected int pedestrianCollision;
    [SerializeField] protected int vehicleCollision;

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
        currentScore = currentScore - scoreReward;
        if(currentScore < 0)
        {
            star = star + 1;
        }
    }
}
