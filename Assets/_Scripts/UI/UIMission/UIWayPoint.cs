using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWayPoint : TuyenMonoBehaviour
{
    [SerializeField] protected UIDestinationWP destinationWP;
    [SerializeField] protected UIArrowWayPoint arrowWayPoint;
    public UIArrowWayPoint ArrowWayPoint => arrowWayPoint;
    protected override void LoadComponents()
    {
        this.LoadUIArrowWayPoint();
        this.LoadUIDestinationWP();
    }

    protected virtual void LoadUIDestinationWP()
    { 
        if (this.destinationWP != null) return;
        destinationWP = GetComponentInChildren<UIDestinationWP>();
        Debug.Log(transform.name + ": LoadUIDestinationWP", gameObject);
    }

    protected virtual void LoadUIArrowWayPoint()
    {
        if (this.arrowWayPoint != null) return;
        arrowWayPoint = GetComponentInChildren<UIArrowWayPoint>();
        Debug.Log(transform.name + ": LoadUIArrowWayPoint", gameObject);
    }
}
