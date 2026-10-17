using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPointMission : BaseMission
{
    [SerializeField] protected Transform playerPosition;
    [SerializeField] protected float minDistanceArea = 300f;
    [SerializeField] protected DestinationManager destinationManager;


    //public static event Action<Transform,float> OnPositionPlayer;
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
        playerPosition = CarManager.Instance.transform;
        //OnPositionPlayer?.Invoke(playerPosition, minDistanceArea);
        destinationManager.GetAreaGoal(playerPosition, minDistanceArea);
    }

    protected void HitDestination(bool isHit)
    {
        if (this.currentState != MissionState.ActiveGameplay) return;
        this.FinishMission(isHit);
    }

    protected override void LoadComponents()
    {
        this.LoadDestinationManager();
    }

    protected virtual void LoadDestinationManager()
    {
        if (destinationManager != null) return;
        destinationManager = FindAnyObjectByType<DestinationManager>();
        Debug.Log(transform.name + ": LoadDestinationManager", gameObject);
    }
}
