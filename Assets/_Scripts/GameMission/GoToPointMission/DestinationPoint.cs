using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationPoint : PoolObj
{
    [SerializeField] protected bool isCompleted = false;

    public static event Action<bool> OnCompleted;
    //protected virtual void OnEnable()
    //{
    //    isCompleted = false;
    //}

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(isCompleted) return;
        BodyCar bodyCar = other.GetComponent<BodyCar>();
        if (bodyCar == null) return;

        CarManager carManager = bodyCar.GetComponentInParent<CarManager>();
        if (carManager == null) return;

        PlayerScore playerScore = carManager.GetComponentInChildren<PlayerScore>();
        if (playerScore != null)
        {
            this.isCompleted = true;

            playerScore.DestinationHit();
            OnCompleted?.Invoke(isCompleted);
            this.DoneDestination();
        }
    }

    protected virtual void DoneDestination()
    {
        despawn.DoDespawn();
        Debug.Log("Done Destination");

    }

    public override string GetName()
    {
        return "DestinationPoint";
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    public void ResetDestinationTrigger()
    {
        isCompleted = false;
    }
}
