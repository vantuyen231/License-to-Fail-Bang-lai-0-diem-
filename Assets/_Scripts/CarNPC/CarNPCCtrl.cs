using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarNPCCtrl : PoolObj
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected CarNPCMoving carNPCMoving;
    [SerializeField] protected CarNPCInfo carNPCInfo;
    [SerializeField] protected TriggerStopCar triggerStopCar;
    public TriggerStopCar TriggerStopCar => triggerStopCar;

    public NavMeshAgent Agent => agent;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNavAgent();
        this.LoadCarNPCMoving();
        this.LoadCarNPCInfo();
        this.LoadTriggerStopCar();
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

    protected virtual void LoadCarNPCMoving()
    {
        if (carNPCMoving != null) return;
        carNPCMoving = GetComponent<CarNPCMoving>();
        Debug.Log(transform.name + ": LoadCarNPCMoving", gameObject);
    }

    protected virtual void LoadCarNPCInfo()
    {
        if (carNPCInfo != null) return;
        carNPCInfo = GetComponent<CarNPCInfo>();
        Debug.Log(transform.name + ": LoadCarNPCInfo", gameObject);
    }

    protected virtual void LoadTriggerStopCar()
    {
        if (triggerStopCar != null) return;
        triggerStopCar = GetComponentInChildren<TriggerStopCar>();
        Debug.Log(transform.name + ": LoadTriggerStopCar", gameObject);
    }
}
