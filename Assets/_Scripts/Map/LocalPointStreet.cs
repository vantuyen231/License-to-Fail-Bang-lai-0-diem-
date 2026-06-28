using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class LocalPointStreet : TuyenMonoBehaviour
{
    [SerializeField] protected LocalPointStreet nextPointInStreet;
    public LocalPointStreet NextPointInStreet => nextPointInStreet;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNextPoint();
    }

    public virtual void LoadNextPoint()
    {
        Transform parent = transform.parent;
        if (parent == null) return;

        int index = transform.GetSiblingIndex();
        int numPoint = parent.childCount;

        if(index +1  < numPoint)
        {
            this.nextPointInStreet = parent.GetChild(index + 1).GetComponent<LocalPointStreet>();
        }
        else
        {
            
            this.nextPointInStreet = parent.GetChild(0).GetComponent<LocalPointStreet>();
        }
    }
    public virtual void SetNextPoint(LocalPointStreet nextPoint)
    {
        this.nextPointInStreet = nextPoint;
    }
}
