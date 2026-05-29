using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossRoadManager : TuyenMonoBehaviour
{
    [SerializeField] protected List<PointPath> pointPath = new List<PointPath>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPointPath();
        this.DistributeLocalPoints();
    }
    
    public virtual void LoadPointPath()
    {
        if(this.pointPath.Count > 0) return;
        foreach(Transform child in transform)
        {
            PointPath pointPaths = child.GetComponent<PointPath>();
            pointPath.Add(pointPaths);
        }
    }
    public virtual void DistributeLocalPoints()
    {
        if (this.pointPath.Count == 0) return;

        foreach (PointPath point in this.pointPath)
        {
            if (point != null)
            {
                point.SetLocalPoints(this.pointPath);
            }
        }
    }

    public virtual List<PointPath> GetPointPaths()
    {
        return this.pointPath;
    }

}
