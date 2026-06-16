using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDespawn : DespawnBase
{
    [SerializeField] protected NPCCtrl ctrl;
    [SerializeField] protected float delay = 5f;
    [SerializeField] protected float countDespawn = 0f;

    protected void OnEnable()
    {
        Invoke(nameof(DoDespawn), this.delay);
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
        if(ctrl.NPCIsHit == false) return;
        countDespawn += Time.deltaTime;
        if(countDespawn >= delay)
        {
            this.DoDespawn();
        }
    }
    public override void DoDespawn()
    {
        NPCSpawnerCtrl.Instance.Spawner.Despawn(ctrl);
    }
}
