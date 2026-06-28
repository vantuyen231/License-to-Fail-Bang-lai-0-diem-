using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetManager : TuyenMonoBehaviour
{
    [SerializeField] protected bool isLoop= true;
    public bool IsLoop => isLoop;
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
        this.SetNextPoint();

    }

    protected virtual void SetNextPoint()
    {
        int index = streetList.Count;
        if(index == 0) return;

        for (int i = 0; i < index; i++)
        {
            if (this.streetList[i] == null) continue;

            if (i + 1 < index)
            {
                Debug.Log(index);

                this.streetList[i].SetNextPoint(this.streetList[i + 1]);
            }
            else
            {
                if(this.isLoop) this.streetList[i].SetNextPoint(null);
                this.streetList[i].SetNextPoint(streetList[0]);
            }
        }
    }
}
