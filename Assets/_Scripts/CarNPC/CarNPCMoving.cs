using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class CarNPCMoving : TuyenMonoBehaviour
{
    [SerializeField] protected LocalPointStreet pointToGo;
    [SerializeField] protected CarNPCCtrl ctrl;
    [SerializeField] protected LocalPointStreet nextPoint;

    [Header("Flags")]
    [SerializeField] protected bool stopCarTrigger = false;
    [SerializeField] protected bool stopCarRaycast = false;
    public bool IsCarStopping => stopCarTrigger || stopCarRaycast;

    [Header("Anit Stuck")]
    [SerializeField] protected float currentTimeStuck = 0f;
    [SerializeField] protected float maxTimeStuck = 5f;
    [SerializeField] protected float currentDistance = 0f;
    [SerializeField] protected float stuckCheckDistance = 1f;


    [Header("Raycast")]
    [SerializeField] protected CarNPCRaycast raycast;
    [SerializeField] protected int maxRaycast = 5;
    public int MaxRaycast => maxRaycast;
    [SerializeField] protected LayerMask obstacleLayer;
    public LayerMask ObstacleLayer => obstacleLayer;

    protected override void Start()
    {
        //if (this.ctrl.Agent == null) this.ctrl.Agent = GetComponent<NavMeshAgent>();

        this.MoveToTarget();
    }
    protected virtual void LateUpdate()
    {
        this.CheckDistanceAndChangePoint();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarCtrl();
        this.LoadCarNPCRaycast();
    }

    protected virtual void LoadCarCtrl()
    {
        if (ctrl != null) return;
        ctrl = GetComponent<CarNPCCtrl>();
        Debug.Log(transform.name + ": LoadCarCtrl", gameObject);
    }

    protected virtual void LoadCarNPCRaycast()
    {
        if (raycast != null) return;
        raycast = GetComponentInChildren<CarNPCRaycast>();
        Debug.Log(transform.name + "LoadCarNPCRaycast", gameObject);
    }

    public virtual void SetStartPointCarNPC(LocalPointStreet streetStartPoint)
    {
        if( streetStartPoint == null) return;   
        this.pointToGo = streetStartPoint;

    }
    protected virtual void MoveToTarget()
    {
        if (this.ctrl.Agent == null) return;


        this.ctrl.Agent.SetDestination(pointToGo.transform.position);
        //Debug.Log("Move");
    }

    protected virtual void LoadNextPoint()
    {
        pointToGo = this.pointToGo.NextPointInStreet;
        nextPoint = pointToGo.NextPointInStreet;
        //Debug.Log("Load");
    }

    protected virtual void CheckDistanceAndChangePoint()
    {
        if (this.ctrl.Agent == null || this.pointToGo == null) return;
        raycast.RaycastCar();
        this.CheckDistancePoint();
        if (!this.ctrl.Agent.pathPending && this.ctrl.Agent.remainingDistance <= this.ctrl.Agent.stoppingDistance)
        {
            this.LoadNextPoint();
            this.MoveToTarget();
            
        }
    }

    protected virtual void CheckDistancePoint()
    {
        currentDistance = ctrl.Agent.remainingDistance;
        if(currentDistance <= stuckCheckDistance)
        {

        }
    }

    protected virtual void CheckTimeStuck()
    {

    }

    public virtual void SetStopByTrigger(bool state)
    {
        this.stopCarTrigger = state;
        this.SetStopCar();
    }

    public virtual void SetStopByRaycast(bool state)
    {
        this.stopCarRaycast = state;
        this.SetStopCar();
    }

    public virtual void SetStopCar()
    {
        if (this.ctrl.Agent == null) return;
        
        this.ctrl.Agent.isStopped = IsCarStopping;
    }


}
