using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGoToPointMission : BaseUIMission
{
    [SerializeField] protected UIDestinationWP destinationWP;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIDestinationWP();
    }

    protected virtual void LoadUIDestinationWP()
    {
        if (this.destinationWP != null) return;
        destinationWP = GetComponentInChildren<UIDestinationWP>();
        Debug.Log(transform.name + ": LoadUIDestinationWP", gameObject);
    }
}
