using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathsManager : TuyenMonoBehaviour
{
    [SerializeField] protected List<CrossRoadManager> crosses = new List<CrossRoadManager>();
    [SerializeField] protected List<PointPath> allPointsInMap = new List<PointPath>();
    [SerializeField] protected float minDistance = 10f; 
    [SerializeField] protected float maxDistance =500f;

    public struct NeighborCrosses
    {
        public CrossRoadManager up;
        public CrossRoadManager down;
        public CrossRoadManager left;
        public CrossRoadManager right;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCrossRoad();
        this.AutoLinkOppositePoints();
    }

    protected virtual void LoadCrossRoad()
    {
        if (this.crosses.Count > 0) return;
        foreach( Transform child in transform )
        {
            CrossRoadManager cross = child.GetComponent<CrossRoadManager>();
            crosses.Add(cross);
        }
    }
    protected virtual void LoadPointPath()
    {
        if (this.crosses.Count < 2) return;



        foreach (CrossRoadManager cross in this.crosses)
        {
            if (cross == null) continue;
            cross.LoadPointPath();
            cross.DistributeLocalPoints();
            allPointsInMap.AddRange(cross.GetPointPaths());
        }

        foreach (PointPath p in allPointsInMap) p.ClearNextCrossRoadPoints();
    }

    protected virtual NeighborCrosses CheckCrossPoint(CrossRoadManager myCross)
    {
        NeighborCrosses neighbors = new NeighborCrosses();
        if (myCross == null) return neighbors;

        Transform myCrossroad = myCross.transform;
        float minCrossDistUp = Mathf.Infinity;
        float minCrossDistDown = Mathf.Infinity;
        float minCrossDistLeft = Mathf.Infinity;
        float minCrossDistRight = Mathf.Infinity;

        foreach (CrossRoadManager targetCross in this.crosses)
        {
            if (targetCross == null || targetCross.transform == myCrossroad) continue;

            float dist = Vector3.Distance(myCrossroad.position, targetCross.transform.position);
            if (dist > this.maxDistance) continue;

            Vector3 dir = targetCross.transform.position - myCrossroad.position;

            if (Mathf.Abs(dir.x) < Mathf.Abs(dir.z)) 
            {
                if (dir.z > 0)
                {
                    if (dist < minCrossDistUp) { minCrossDistUp = dist; neighbors.up = targetCross; }
                }
                else
                {
                    if (dist < minCrossDistDown) { minCrossDistDown = dist; neighbors.down = targetCross; }
                }
            }
            else 
            {
                if (dir.x > 0)
                {
                    if (dist < minCrossDistRight) { minCrossDistRight = dist; neighbors.right = targetCross; }
                }
                else
                {
                    if (dist < minCrossDistLeft) { minCrossDistLeft = dist; neighbors.left = targetCross; }
                }
            }
        }
        return neighbors;
    }


    protected virtual void CasePoint(PointPath pointA, Transform myCrossroad, NeighborCrosses neighbors)
    {
        float threshold = 1.5f;

        bool pointA_IsRight = pointA.transform.position.x > myCrossroad.position.x;
        bool pointA_IsTop = pointA.transform.position.z > myCrossroad.position.z;

        for (int j = 0; j < allPointsInMap.Count; j++)
        {
            PointPath pointB = allPointsInMap[j];
            if (pointB == null || pointB.transform.parent == myCrossroad) continue;

            Transform targetCrossroad = pointB.transform.parent;

            if (!pointA_IsTop && targetCrossroad == (neighbors.down != null ? neighbors.down.transform : null))
            {
                float deltaX = Mathf.Abs(pointA.transform.position.x - pointB.transform.position.x);
                bool pointB_IsTop = pointB.transform.position.z > targetCrossroad.position.z;

                if (deltaX < threshold && pointB_IsTop) pointA.AddNextCrossRoadPoint(pointB);
            }

            if (pointA_IsTop && targetCrossroad == (neighbors.up != null ? neighbors.up.transform : null))
            {
                float deltaX = Mathf.Abs(pointA.transform.position.x - pointB.transform.position.x);
                bool pointB_IsTop = pointB.transform.position.z > targetCrossroad.position.z;

                if (deltaX < threshold && !pointB_IsTop) pointA.AddNextCrossRoadPoint(pointB);
            }

            if (pointA_IsRight && targetCrossroad == (neighbors.right != null ? neighbors.right.transform : null))
            {
                float deltaZ = Mathf.Abs(pointA.transform.position.z - pointB.transform.position.z);
                bool pointB_IsRight = pointB.transform.position.x > targetCrossroad.position.x;

                if (deltaZ < threshold && !pointB_IsRight) pointA.AddNextCrossRoadPoint(pointB);
            }

            if (!pointA_IsRight && targetCrossroad == (neighbors.left != null ? neighbors.left.transform : null))
            {
                float deltaZ = Mathf.Abs(pointA.transform.position.z - pointB.transform.position.z);
                bool pointB_IsRight = pointB.transform.position.x > targetCrossroad.position.x;

                if (deltaZ < threshold && pointB_IsRight) pointA.AddNextCrossRoadPoint(pointB);
            }
        }
    }



    public virtual void AutoLinkOppositePoints()
    {
        this.LoadPointPath();

        foreach (CrossRoadManager cross in this.crosses)
        {
            if (cross == null) continue;

            NeighborCrosses currentNeighbors = this.CheckCrossPoint(cross);

            List<PointPath> localPoints = cross.GetPointPaths();
            if (localPoints == null) continue;

            foreach (PointPath pointA in localPoints)
            {
                if (pointA == null) continue;

                this.CasePoint(pointA, cross.transform, currentNeighbors);
            }
        }
    }
    //public virtual void AutoLinkOppositePoints()
    //{
    //    this.LoadPointPath();

    //    float threshold = 1.5f; 

    //    for (int i = 0; i < allPointsInMap.Count; i++)
    //    {
    //        PointPath pointA = allPointsInMap[i];
    //        if (pointA == null) continue;

    //        Transform myCrossroad = pointA.transform.parent;
    //        if (myCrossroad == null) continue;

    //        CrossRoadManager crossUp = null; float minCrossDistUp = Mathf.Infinity;
    //        CrossRoadManager crossDown = null; float minCrossDistDown = Mathf.Infinity;
    //        CrossRoadManager crossLeft = null; float minCrossDistLeft = Mathf.Infinity;
    //        CrossRoadManager crossRight = null; float minCrossDistRight = Mathf.Infinity;

    //        foreach (CrossRoadManager targetCross in this.crosses)
    //        {
    //            if (targetCross == null || targetCross.transform == myCrossroad) continue;

    //            float dist = Vector3.Distance(myCrossroad.position, targetCross.transform.position);
    //            if (dist > this.maxDistance) continue;

    //            Vector3 dir = targetCross.transform.position - myCrossroad.position;

    //            if (Mathf.Abs(dir.x) < Mathf.Abs(dir.z))
    //            {
    //                if (dir.z > 0) 
    //                {
    //                    if (dist < minCrossDistUp) { minCrossDistUp = dist; crossUp = targetCross; }
    //                }
    //                else 
    //                {
    //                    if (dist < minCrossDistDown) { minCrossDistDown = dist; crossDown = targetCross; }
    //                }
    //            }
    //            else 
    //            {
    //                if (dir.x > 0) 
    //                {
    //                    if (dist < minCrossDistRight) { minCrossDistRight = dist; crossRight = targetCross; }
    //                }
    //                else 
    //                {
    //                    if (dist < minCrossDistLeft) { minCrossDistLeft = dist; crossLeft = targetCross; }
    //                }
    //            }
    //        }

    //        bool pointA_IsRight = pointA.transform.position.x > myCrossroad.position.x;
    //        bool pointA_IsTop = pointA.transform.position.z > myCrossroad.position.z;

    //        for (int j = 0; j < allPointsInMap.Count; j++)
    //        {
    //            PointPath pointB = allPointsInMap[j];
    //            if (pointB == null || pointB.transform.parent == myCrossroad) continue;

    //            Transform targetCrossroad = pointB.transform.parent;

    //            if (!pointA_IsTop && targetCrossroad == (crossDown != null ? crossDown.transform : null))
    //            {
    //                float deltaX = Mathf.Abs(pointA.transform.position.x - pointB.transform.position.x);
    //                bool pointB_IsTop = pointB.transform.position.z > targetCrossroad.position.z;

    //                if (deltaX < threshold && pointB_IsTop)
    //                {
    //                    pointA.AddNextCrossRoadPoint(pointB);
    //                }
    //            }

    //            if (pointA_IsTop && targetCrossroad == (crossUp != null ? crossUp.transform : null))
    //            {
    //                float deltaX = Mathf.Abs(pointA.transform.position.x - pointB.transform.position.x);
    //                bool pointB_IsTop = pointB.transform.position.z > targetCrossroad.position.z;

    //                if (deltaX < threshold && !pointB_IsTop)
    //                {
    //                    pointA.AddNextCrossRoadPoint(pointB);
    //                }
    //            }

    //            if (pointA_IsRight && targetCrossroad == (crossRight != null ? crossRight.transform : null))
    //            {
    //                float deltaZ = Mathf.Abs(pointA.transform.position.z - pointB.transform.position.z);
    //                bool pointB_IsRight = pointB.transform.position.x > targetCrossroad.position.x;

    //                if (deltaZ < threshold && !pointB_IsRight)
    //                {
    //                    pointA.AddNextCrossRoadPoint(pointB);
    //                }
    //            }

    //            if (!pointA_IsRight && targetCrossroad == (crossLeft != null ? crossLeft.transform : null))
    //            {
    //                float deltaZ = Mathf.Abs(pointA.transform.position.z - pointB.transform.position.z);
    //                bool pointB_IsRight = pointB.transform.position.x > targetCrossroad.position.x;

    //                if (deltaZ < threshold && pointB_IsRight)
    //                {
    //                    pointA.AddNextCrossRoadPoint(pointB);
    //                }
    //            }
    //        }
    //    }
    //}


}
