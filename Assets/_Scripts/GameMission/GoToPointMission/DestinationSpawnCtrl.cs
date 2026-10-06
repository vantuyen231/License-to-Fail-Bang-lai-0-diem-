using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationSpawnCtrl : TuyenSingleton<DestinationSpawnCtrl>
{
    [SerializeField] protected DestinationSpawner destinationSpawner;
    public DestinationSpawner DestinationSpwan => destinationSpawner;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDestinationSpawner();
    }
    protected virtual void LoadDestinationSpawner()
    {
        if (this.destinationSpawner != null) return;
        destinationSpawner = GetComponent<DestinationSpawner>();
        Debug.Log(transform.name + ": LoadDestinationSpawner", gameObject);
    }
}
