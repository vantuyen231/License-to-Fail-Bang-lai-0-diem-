using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : TuyenMonoBehaviour
{
    [SerializeField] protected List<NPCRagdoll> npcRagdolls = new();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNPC();
    }

    protected virtual void LoadNPC()
    {
        if(npcRagdolls.Count > 0) return;
        foreach(Transform child in transform)
        {
            NPCRagdoll npcRagdoll = child.GetComponent<NPCRagdoll>();
            npcRagdolls.Add(npcRagdoll);
        }
    }
}
