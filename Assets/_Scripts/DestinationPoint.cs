using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationPoint : TuyenMonoBehaviour
{
    [SerializeField] protected int score = 100; 

    protected virtual void OnTriggerEnter(Collider other)
    {
        this.DoneDestination();
    }

    protected virtual void DoneDestination()
    {
        //GameManager.Instance.ScoreMission = score;
        Debug.Log("Done Destination");
        GameManager.Instance.UpdateScorePlayer(score);
    }
}
