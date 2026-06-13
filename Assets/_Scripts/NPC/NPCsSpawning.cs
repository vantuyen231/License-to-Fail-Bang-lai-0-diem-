using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsSpawning : TuyenMonoBehaviour
{
    [SerializeField] protected float timer = 0f;
    [SerializeField] protected float delay = 3f;

    protected virtual void FixedUpdate()
    {
        this.RunTime();
    }

    protected virtual void RunTime()
    {
        timer += Time.deltaTime;
        if(timer < delay ) return;
        timer = 0;

        //NPCCtrl npcPrefab = this.
    }
}
