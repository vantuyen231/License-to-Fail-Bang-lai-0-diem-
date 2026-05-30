using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPath : TuyenMonoBehaviour
{
    [SerializeField] protected List<PointPath> nextCrossRoadPoints = new List<PointPath>();
    [SerializeField] protected List<PointPath> localPoints;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        
    }

    public virtual void SetLocalPoints(List<PointPath> allPoints)
    {
        this.localPoints = new List<PointPath>(allPoints);

        this.localPoints.Remove(this);
    }

    public virtual void ClearNextCrossRoadPoints()
    {
        this.nextCrossRoadPoints.Clear();
    }

    public virtual void AddNextCrossRoadPoint(PointPath targetPoint)
    {
        if (targetPoint == null) return;
        if (!this.nextCrossRoadPoints.Contains(targetPoint))
        {
            this.nextCrossRoadPoints.Add(targetPoint);
        }
    }

    public virtual List<PointPath> GetNextCrossRoadPoints()
    {
        return this.nextCrossRoadPoints;
    }
}
