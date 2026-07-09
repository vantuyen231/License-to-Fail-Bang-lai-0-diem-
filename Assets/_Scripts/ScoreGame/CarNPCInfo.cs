using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPCInfo : InfoObjectHit
{
    [SerializeField] protected bool isHitCarNPC = false;
    public bool IsHitCarNPC => isHitCarNPC;
    protected override void Start()
    {
        if(objectDataSO != null) return;
        Debug.LogWarning("Load SO in CarNPC");
    }

    public virtual void CarNPCCollision()
    {
        isHitCarNPC = true;
    }

    protected virtual void OnEnable()
    {
        isHitCarNPC = false;
    }
}
