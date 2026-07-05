using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDespawn : DespawnBase
{
    [SerializeField] protected NPCCtrl ctrl;
    [SerializeField] protected float delay = 5f;
    [SerializeField] protected float countDespawn = 0f;

    protected virtual void FixedUpdate()
    {
        this.TimeToDeSpawn();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNPCCtrl();
    }

    protected virtual void LoadNPCCtrl()
    {
        if(ctrl != null) return;
        ctrl = transform.parent.GetComponent<NPCCtrl>();
    }

    protected virtual void TimeToDeSpawn()
    {
        if(ctrl.NpcRagdoll.IsRagdoll == false) return;
        countDespawn += Time.fixedDeltaTime;
        if(countDespawn >= delay)
        {
            countDespawn = 0f;
            this.DoDespawn();
        }
    }
    public override void DoDespawn()
    {
        NPCSpawnerCtrl.Instance.Spawner.Despawn(ctrl);
    }

    public virtual void OutAreaPlayer()
    {
        if (this.ctrl != null && this.ctrl.NpcRagdoll != null && this.ctrl.NpcRagdoll.IsRagdoll) return;

        this.DoDespawn();
    }

}
