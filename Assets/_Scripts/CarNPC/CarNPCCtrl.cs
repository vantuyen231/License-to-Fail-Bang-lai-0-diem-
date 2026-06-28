using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarNPCCtrl : PoolObj
{
    [SerializeField] protected NavMeshAgent agent;
    public NavMeshAgent Agent => agent;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNavAgent();
    }
    public override string GetName()
    {
        return "CarNPC";
    }

    protected virtual void LoadNavAgent()
    {
        if(agent != null) return;
        agent = GetComponent<NavMeshAgent>();
        Debug.Log(transform.name + ": LoadNavAgent", gameObject);
    }

}
