using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPointMission : BaseMission
{
    [SerializeField] protected Vector3 playerPosition;
    [SerializeField] protected float minDistanceArea = 100f;


    public static event Action<Vector3,float> OnPositionPlayer;
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
        playerPosition = CarManager.Instance.transform.position;
        OnPositionPlayer?.Invoke(playerPosition, minDistanceArea);
    }

    protected void HitDestination(bool isHit)
    {
        if (this.currentState != MissionState.ActiveGameplay) return;
        this.FinishMission(isHit);
    }
}
