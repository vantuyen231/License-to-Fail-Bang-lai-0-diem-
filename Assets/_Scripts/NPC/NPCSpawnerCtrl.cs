using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnerCtrl : TuyenSingleton<NPCSpawnerCtrl>
{
    [SerializeField] protected NPCSpawner spawner;
    public NPCSpawner Spawner => spawner;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNPCSpawner();
    }

    protected virtual void LoadNPCSpawner()
    {
        if(spawner != null) return;
        spawner = GetComponent<NPCSpawner>();
        Debug.Log(transform.name + ": LoadNPCSpawner", gameObject);
    }

}
