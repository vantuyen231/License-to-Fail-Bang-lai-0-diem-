using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSpawnCtrl : TuyenSingleton<PoliceSpawnCtrl>
{
    [SerializeField] protected PoliceCarSpawner policeSpawner;

    public PoliceCarSpawner PoliceSpawner => policeSpawner;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoliceCarSpawner();
    }

    protected virtual void LoadPoliceCarSpawner()
    { 
        if (this.policeSpawner != null) return;
        policeSpawner = GetComponent<PoliceCarSpawner>();
        Debug.Log(transform.name + ": LoadPoliceCarSpawner", gameObject);
    }
}
