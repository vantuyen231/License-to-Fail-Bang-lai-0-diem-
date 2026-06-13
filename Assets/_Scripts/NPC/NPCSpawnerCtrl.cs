using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnerCtrl : TuyenSingleton<NPCSpawnerCtrl>
{
    [SerializeField] protected NPCSpawner spawn;
    public NPCSpawner Spawner => spawn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNPCSpawner();
    }

    protected virtual void LoadNPCSpawner()
    {
        if(spawn != null) return;
        spawn = GetComponent<NPCSpawner>();
        Debug.Log("Load " + spawn);
    }

}
