using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNPCInfo : InfoObjectHit
{
    //[SerializeField] protected HitObjectDataSO carDataSO;
    //public HitObjectDataSO CarDataSO => carDataSO;
    protected override void Start()
    {
        if(objectDataSO != null) return;
        Debug.LogWarning("Load SO in CarNPC");
    }
}
