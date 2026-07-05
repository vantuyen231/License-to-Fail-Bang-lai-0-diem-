using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : TuyenMonoBehaviour
{
    [SerializeField] protected int maxScore = 12;
    [SerializeField] protected int currentScore;
    [SerializeField] protected int star;

    public virtual void LoadCroceHitNPC()
    {
        Debug.Log("Hit NPC");
    }

    public void LoadCroceHitCarNPC()
    {
        Debug.Log("Hit carNPC");
    }
}
