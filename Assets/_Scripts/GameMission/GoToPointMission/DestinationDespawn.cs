using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationDespawn : DespawnBase
{
    [SerializeField] protected DestinationPoint destinationPoint;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDestinationPoint();
    }

    protected virtual void LoadDestinationPoint()
    {
        if (this.destinationPoint != null) return;
        this.destinationPoint = transform.GetComponentInParent<DestinationPoint>();
        Debug.Log(transform.name + ": LoadDestinationPoint", gameObject);
    }

    public override void DoDespawn()
    {
        Debug.Log(transform.name + "despawn");
        DestinationSpawnCtrl.Instance.DestinationSpwan.Despawn(destinationPoint);
    }
}
