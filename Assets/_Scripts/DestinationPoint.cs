using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationPoint : TuyenMonoBehaviour
{
    [SerializeField] protected bool isCompleted = false;

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

            this.DoneDestination();
        }
    }

    protected virtual void DoneDestination()
    {
        Debug.Log("Done Destination");

    }

    protected virtual void OnEnable()
    {
        isCompleted = false;
    }
}
