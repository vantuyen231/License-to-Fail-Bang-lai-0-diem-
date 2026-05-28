using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathsManager : TuyenMonoBehaviour
{
    [SerializeField] protected List<CrossRoadManager> crosses = new List<CrossRoadManager>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCrossRoad();
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
}
