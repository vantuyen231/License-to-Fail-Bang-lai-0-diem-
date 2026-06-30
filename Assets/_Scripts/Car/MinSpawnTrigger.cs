using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinSpawnTrigger : MonoBehaviour
{
    [SerializeField] protected List<PointPath> pointsNPCInMinRange = new List<PointPath>();
    public List<PointPath> PointsInMinRanger => pointsNPCInMinRange;

    [SerializeField] protected List<LocalPointStreet> pointStreetsInMinRange = new List<LocalPointStreet>();
    public List<LocalPointStreet> PointStreetInMinRange => pointStreetsInMinRange;

    protected virtual void OnTriggerEnter(Collider other)
    {
        this.AddPointPath(other);

        this.AddPointStreet(other);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        this.RemovePointPath(other);

        this.RemovePointStreet(other);
    }

    protected virtual void AddPointPath(Collider other)
    {
        PointPath pointPath = other.GetComponent<PointPath>();
        if (pointPath != null && !pointsNPCInMinRange.Contains(pointPath))
        {
            pointsNPCInMinRange.Add(pointPath);
        }
    }

    protected virtual void AddPointStreet(Collider other)
    {
        LocalPointStreet pointStreet = other.GetComponent<LocalPointStreet>();
        if (pointStreet != null && !pointStreetsInMinRange.Contains(pointStreet))
        {
            pointStreetsInMinRange.Add(pointStreet);
        }
    }

    protected virtual void RemovePointPath(Collider other)
    {
        PointPath pointPath = other.GetComponent<PointPath>();
        if (pointPath != null && pointsNPCInMinRange.Contains(pointPath))
        {
            pointsNPCInMinRange.Remove(pointPath);
        }
    }

    protected virtual void RemovePointStreet(Collider other)
    {
        LocalPointStreet pointStreet = other.GetComponent<LocalPointStreet>();
        if (pointStreet != null && pointStreetsInMinRange.Contains(pointStreet))
        {
            pointStreetsInMinRange.Remove(pointStreet);
        }
    }
}
