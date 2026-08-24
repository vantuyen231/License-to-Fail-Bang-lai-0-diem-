using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceCarCtrl : PoolObj
{
    [SerializeField] protected PoliceCarMoving policeMoving;
    [SerializeField] protected AITargetPlayer targetPlayer;


    #region(Load Components)
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoliceMoving();
        this.LoadAITargetPlayer();
    }

    protected virtual void LoadPoliceMoving()
    {
        if (policeMoving != null) return;
        policeMoving = GetComponent<PoliceCarMoving>();
        Debug.Log(transform.name + ": LoadPoliceMoving", gameObject);
    }

    protected virtual void LoadAITargetPlayer()
    {
        if (targetPlayer != null) return;
        targetPlayer = GetComponent<AITargetPlayer>();
        Debug.Log(transform.name + ": LoadAITargetPlayer", gameObject);
    }
    #endregion

 

    public override string GetName()
    {
        return "PoliceCar";
    }

}
