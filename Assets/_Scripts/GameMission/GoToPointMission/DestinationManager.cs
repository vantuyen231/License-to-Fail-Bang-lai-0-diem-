using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationManager : TuyenMonoBehaviour
{
    [SerializeField] protected List<DestinationArea> destinationAreas = new List<DestinationArea>();
    [SerializeField] protected List<DestinationArea> choiceArea = new List<DestinationArea>();
    [SerializeField] protected DestinationArea areaSpawn;
    [SerializeField] protected DestinationSpawning destinationSpawning;


    protected void OnEnable()
    {
        GoToPointMission.OnPositionPlayer += GetAreaGoal;
    }

    protected void OnDisable()
    {
        GoToPointMission.OnPositionPlayer -= GetAreaGoal;
    }

    protected void GetAreaGoal(Vector3 player, float minDistance)
    {
        choiceArea.Clear();
        Debug.Log("CarPosition: "+ player);
        if(destinationAreas == null) return;

        foreach (DestinationArea area in destinationAreas)
        {
            float distanceArea = Vector3.Distance(player,area.transform.position);
            if (distanceArea > minDistance)
            {
                choiceArea.Add(area);
            }
        }
        int indexer = Random.Range(0,choiceArea.Count);
        areaSpawn = choiceArea[indexer];
        this.destinationSpawning.GetPointSpawnGoal(areaSpawn);
    }
    protected override void LoadComponents()
    {
        this.LoadDestinationArea();
        this.LoadDestinationSpawning();
    }

    protected void LoadDestinationArea()
    {
        this.destinationAreas.Clear();
        this.destinationAreas.AddRange(GetComponentsInChildren<DestinationArea>());
    }

    protected virtual void LoadDestinationSpawning()
    {
        if (destinationSpawning != null) return;
        destinationSpawning = FindAnyObjectByType<DestinationSpawning>();
        Debug.Log(transform.name + ": LoadDestinationSpawning", gameObject);
    }
}
