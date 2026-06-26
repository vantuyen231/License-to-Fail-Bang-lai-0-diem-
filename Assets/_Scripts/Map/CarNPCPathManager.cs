using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPCPathManager : TuyenMonoBehaviour
{
    [SerializeField] protected List<StreetManager> streetManagers = new List<StreetManager>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStreetManager();
    }

    protected virtual void LoadStreetManager()
    {
        this.streetManagers.Clear();
        this.streetManagers.AddRange(GetComponentsInChildren<StreetManager>());
        foreach(StreetManager child in this.streetManagers)
        {
            child.LoadLocalPointStreet();
        }
    }
}
