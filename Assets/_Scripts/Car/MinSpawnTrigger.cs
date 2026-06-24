using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinSpawnTrigger : MonoBehaviour
{
    [SerializeField] protected List<PointPath> pointsInMinRange = new List<PointPath>();
    public List<PointPath> PointsInMinRanger => pointsInMinRange;

    protected virtual void OnTriggerEnter(Collider other)
    {
        PointPath pointPath = other.GetComponent<PointPath>(); 
        if(pointPath != null && !pointsInMinRange.Contains(pointPath))
        {
            pointsInMinRange.Add(pointPath);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        PointPath pointPath = other.GetComponent<PointPath>();
        if (pointPath != null && pointsInMinRange.Contains(pointPath))
        {
            pointsInMinRange.Remove(pointPath);
        }
    }
}
