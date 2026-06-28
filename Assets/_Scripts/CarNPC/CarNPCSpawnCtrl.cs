using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPCSpawnCtrl : TuyenSingleton<CarNPCSpawnCtrl>
{
    [SerializeField] protected CarNPCSpawner carNPCSpawner;
    public CarNPCSpawner CarNPCSpawner => carNPCSpawner;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarNPCSpawner();
    }
    protected virtual void LoadCarNPCSpawner()
    {
        if (this.carNPCSpawner != null) return;
        carNPCSpawner = GetComponent<CarNPCSpawner>();
        Debug.Log(transform.name + ": LoadCarNPCSpawner", gameObject);
    }
}