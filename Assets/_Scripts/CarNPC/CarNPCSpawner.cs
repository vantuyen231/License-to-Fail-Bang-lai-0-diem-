using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPCSpawner : Spawner<CarNPCCtrl>
{
    [SerializeField] protected CarNPCPrefabs carNPCPrefabs;

    protected override void LoadComponents()
    {
        base.LoadComponents();

    }

    protected virtual void LoadCarNPCPrefabs()
    {
        if(carNPCPrefabs != null) return;
    }
}
