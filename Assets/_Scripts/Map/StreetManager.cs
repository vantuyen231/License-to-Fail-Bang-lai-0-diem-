using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetManager : TuyenMonoBehaviour
{
    [SerializeField] protected List<LocalPointStreet> streetList = new List<LocalPointStreet>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLocalPointStreet();
    }

    public virtual void LoadLocalPointStreet()
    {
        streetList.Clear();
        this.streetList.AddRange(GetComponentsInChildren<LocalPointStreet>());
        foreach (LocalPointStreet child in this.streetList)
        {
            child.LoadNextPoint();
        }

    }
}
