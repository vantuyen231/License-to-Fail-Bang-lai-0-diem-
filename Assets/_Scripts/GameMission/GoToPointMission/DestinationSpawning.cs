using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationSpawning : TuyenMonoBehaviour
{
    [SerializeField] protected DestinationSpawnCtrl destinationSpawnCtrl;
    public DestinationSpawnCtrl DestinationSpawnCtrl => destinationSpawnCtrl;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDestinationSpawnCtrl();
    }
    protected virtual void LoadDestinationSpawnCtrl()
    {
        if (this.destinationSpawnCtrl != null) return;
        destinationSpawnCtrl = GetComponent<DestinationSpawnCtrl>();
        Debug.Log(transform.name + ": LoadDestinationSpawnCtrl", gameObject);
    }
}
