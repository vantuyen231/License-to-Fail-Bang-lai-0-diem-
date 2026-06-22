using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSpawnTrigger : MonoBehaviour
{
    protected virtual void OnTriggerExit(Collider other)
    {
        NPCDespawn npcDespawn = other.gameObject.GetComponentInParent<NPCDespawn>();
        if (npcDespawn == null)
        {
            NPCCtrl npcCtrl = other.gameObject.GetComponentInParent<NPCCtrl>();
            if (npcCtrl != null)
            {
                npcDespawn = npcCtrl.GetComponentInChildren<NPCDespawn>();
            }
        }

        if (npcDespawn != null)
        {
            npcDespawn.OutAreaPlayer();
        }
    }
}
