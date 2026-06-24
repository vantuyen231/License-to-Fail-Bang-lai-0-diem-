using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSpawnTrigger : TuyenMonoBehaviour
{
    [SerializeField] protected List<PointPath> pointsInMaxRange = new List<PointPath>();
    public List<PointPath> PointsInMaxRange => pointsInMaxRange;

    protected virtual void OnTriggerEnter(Collider other)
    {
        PointPath pointPath = other.GetComponent<PointPath>();
        if (pointPath != null && !pointsInMaxRange.Contains(pointPath))
        {
            pointsInMaxRange.Add(pointPath);
        }
    }

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

        PointPath pointPath = other.GetComponent<PointPath>();
        if (pointPath != null && pointsInMaxRange.Contains(pointPath))
        {
            pointsInMaxRange.Remove(pointPath);
        }
    }
}
