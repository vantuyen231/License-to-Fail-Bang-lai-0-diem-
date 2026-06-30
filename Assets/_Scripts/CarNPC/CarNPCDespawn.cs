using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPCDespawn : DespawnBase
{
    [SerializeField] protected CarNPCCtrl carCtrl;

    [SerializeField] protected float timeDespawn = 5;
    [SerializeField] protected float coolDown = 0;

    protected virtual void FixedUpdate()
    {
        //this.DeSpawnTimeOut();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarNPCCtrl();
    }

    protected virtual void LoadCarNPCCtrl()
    {
        if(this.carCtrl != null) return;
        carCtrl = transform.parent.GetComponent<CarNPCCtrl>();
        Debug.Log(transform.name + ": LoadCarNPCCtrl ", gameObject);
    }

    protected virtual void DeSpawnTimeOut()
    {
        coolDown += Time.fixedDeltaTime;
        if (coolDown < timeDespawn) return;
        this.DoDespawn();
        coolDown = 0;
    }
    public override void DoDespawn()
    {
        Debug.Log("DoDespawn");
        CarNPCSpawnCtrl.Instance.CarNPCSpawner.Despawn(carCtrl);
    }

    public virtual void OutAreaPlayer()
    {
        //if (this.carCtrl != null && this.carCtrl != null && this.carCtrl) return;

        this.DoDespawn();
    }
}
