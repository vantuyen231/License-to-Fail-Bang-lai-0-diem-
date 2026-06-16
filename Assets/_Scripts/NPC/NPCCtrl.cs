using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCtrl : PoolObj
{
    [SerializeField] protected NPCRagdoll npcRagdoll;
    [SerializeField] protected NPCMoving npcMoving;

    [SerializeField] protected bool npcIsHit;
    public bool NPCIsHit => npcIsHit;


    protected void FixedUpdate()
    {
        OffMoving();
    }
    public override string GetName()
    {
        return "NPC";
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNPCRagdoll();
        this.LoadNPCMoving();
    }

    protected virtual void LoadNPCRagdoll()
    {
        if (npcRagdoll != null) return;
        npcRagdoll = GetComponent<NPCRagdoll>();
    }

    protected virtual void LoadNPCMoving()
    {
        if (npcMoving != null) return;
        npcMoving = GetComponent<NPCMoving>();
    }


    protected virtual void OffMoving()
    {
        npcIsHit = this.npcRagdoll.IsRagdoll;
        if (npcIsHit)
        {
            npcMoving.enabled = false;
        }
    }


}
