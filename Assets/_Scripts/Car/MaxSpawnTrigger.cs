using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSpawnTrigger : TuyenMonoBehaviour
{
    [SerializeField] protected List<PointPath> pointsNPCInMaxRange = new List<PointPath>();
    public List<PointPath> PointsInMaxRange => pointsNPCInMaxRange;

    [SerializeField] protected List<LocalPointStreet> pointStreetsInMaxRange = new List<LocalPointStreet>();
    public List <LocalPointStreet> PointStreets => pointStreetsInMaxRange;

    protected virtual void OnTriggerEnter(Collider other)
    {
        this.AddPointPath(other);

        this.AddPointStreet(other);
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
                if (npcDespawn != null)
                {
                    npcDespawn.OutAreaPlayer();
                }
            }
        }

        this.RemovePointPath(other);

        this.RemovePointStreet(other);
    }

    protected virtual void AddPointPath(Collider other)
    {
        PointPath pointPath = other.GetComponent<PointPath>();
        if (pointPath != null && !pointsNPCInMaxRange.Contains(pointPath))
        {
            pointsNPCInMaxRange.Add(pointPath);
        }
    }

    protected virtual void AddPointStreet(Collider other)
    {
        LocalPointStreet pointStreet = other.GetComponent<LocalPointStreet>();
        if (pointStreet != null && !pointStreetsInMaxRange.Contains(pointStreet))
        {
            pointStreetsInMaxRange.Add(pointStreet);
        }
    }

    protected void RemovePointPath(Collider other)
    {
        PointPath pointPath = other.GetComponent<PointPath>();
        if (pointPath != null && pointsNPCInMaxRange.Contains(pointPath))
        {
            pointsNPCInMaxRange.Remove(pointPath);
        }
    }

    protected virtual void RemovePointStreet(Collider other)
    {
        LocalPointStreet pointStreet = other.GetComponent<LocalPointStreet>();
        if (pointStreet != null && pointStreetsInMaxRange.Contains(pointStreet))
        {
            pointStreetsInMaxRange.Remove(pointStreet);
        }
    }
}
