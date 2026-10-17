using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDeliveryMission : TimeBaseMission
{
    [SerializeField] protected Transform playerPosition;
    [SerializeField] protected float minDistanceArea = 300f;
    [SerializeField] protected DestinationManager destinationManager;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        missionType = MissionType.TimedDelivery;
        timeMission = 100f;
        this.LoadDestinationManager();
    }


    private void OnEnable()
    {
        DestinationPoint.OnCompleted += HitDestination;
    }
    private void OnDisable()
    {
        DestinationPoint.OnCompleted -= HitDestination;
    }

    protected override void OnStartMission()
    {
        base.OnStartMission();
        playerPosition = CarManager.Instance.transform;
        destinationManager.GetAreaGoal(playerPosition, minDistanceArea);
    }

    protected void HitDestination(bool isHit)
    {
        if (this.currentState != MissionState.ActiveGameplay) return;
        this.FinishMission(isHit);
    }


    protected virtual void LoadDestinationManager()
    {
        if (destinationManager != null) return;
        destinationManager = FindAnyObjectByType<DestinationManager>();
        Debug.Log(transform.name + ": LoadDestinationManager", gameObject);
    }
}
