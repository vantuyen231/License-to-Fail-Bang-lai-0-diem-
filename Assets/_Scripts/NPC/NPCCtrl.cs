using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCtrl : PoolObj
{
    [SerializeField] protected bool npcIsHit;
    public bool NPCIsHit => npcIsHit;

    [SerializeField] protected NPCRagdoll npcRagdoll;
    public NPCRagdoll NpcRagdoll => npcRagdoll;
    [SerializeField] protected NPCMoving npcMoving;
    [SerializeField] protected NPCGameInfo npc;

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
        this.LoadNPCGameInfo();
    }

    protected virtual void LoadNPCRagdoll()
    {
        if (npcRagdoll != null) return;
        npcRagdoll = GetComponent<NPCRagdoll>();
        Debug.Log(transform.name + ": LoadNPCRagdoll", gameObject);
    }

    protected virtual void LoadNPCGameInfo()
    {
        if (npc != null) return;
        npc = GetComponent<NPCGameInfo>();
        Debug.Log(transform.name + ": LoadNPCGameInfo", gameObject);
    }

    protected virtual void LoadNPCMoving()
    {
        if (npcMoving != null) return;
        npcMoving = GetComponent<NPCMoving>();
        Debug.Log(transform.name + ": LoadNPCMoving", gameObject);
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
