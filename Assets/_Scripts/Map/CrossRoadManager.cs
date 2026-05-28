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
    }
    
    protected virtual void LoadPointPath()
    {
        if(this.pointPath.Count > 0) return;
        foreach(Transform child in transform)
        {
            PointPath pointPaths = child.GetComponent<PointPath>();
            pointPath.Add(pointPaths);
        }
    }
}
