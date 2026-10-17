using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NDestinationMission : BaseMission
{
    [SerializeField] protected int maxDestinationSpawn = 3;
    [SerializeField] protected int currentDestination = 0;
    [SerializeField] protected Transform playerPosition;
    [SerializeField] protected float minDistanceArea = 300f;
    [SerializeField] protected DestinationManager destinationManager;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        missionType = MissionType.SequentialRoute;
        missionName = "Go around the City";
        this.LoadDestinationManager();
    }

    protected virtual void OnEnable()
    {
        DestinationPoint.OnCompleted += CanSpawnDestination;
    }

    protected void OnDisable()
    {
        DestinationPoint.OnCompleted -= CanSpawnDestination;
    }

    protected override void OnStartMission()
    {
        playerPosition = CarManager.Instance.transform;
        destinationManager.GetAreaGoal(playerPosition, minDistanceArea);

    }

    protected virtual void LoadDestinationManager()
    {
        if (destinationManager != null) return;
        destinationManager = FindAnyObjectByType<DestinationManager>();
        Debug.Log(transform.name + ": LoadDestinationManager", gameObject);
    }

    protected void CanSpawnDestination(bool isDone)
    {
        if(isDone == false) return;
        currentDestination += 1;
        if (currentDestination >= maxDestinationSpawn)
        {
            this.FinishMission(isDone); 
            return;
        }
        destinationManager.GetAreaGoal(playerPosition, minDistanceArea);

    }
}
