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

    [Header("Raycast")]
    [SerializeField] protected CarNPCRaycast raycast;
    [SerializeField] protected int maxRaycast = 5;
    [SerializeField] protected LayerMask obstacleLayer;

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
        //Debug.Log("Load");
    }

    protected virtual void CheckDistanceAndChangePoint()
    {
        if (this.ctrl.Agent == null || this.pointToGo == null) return;
        this.CarNPCRaycast();
        if (!this.ctrl.Agent.pathPending && this.ctrl.Agent.remainingDistance <= this.ctrl.Agent.stoppingDistance)
        {
            this.LoadNextPoint();
            this.MoveToTarget();
            
        }
    }

    protected virtual void CarNPCRaycast()
    {
        if(raycast == null ) return;
        RaycastHit hit;

        if(Physics.Raycast(raycast.transform.position, raycast.transform.forward, out hit, maxRaycast, obstacleLayer))
        {
            Debug.DrawLine(raycast.transform.position, hit.point, Color.red);
        }
        else
        {
            Debug.DrawLine(raycast.transform.position,raycast.transform.position+(raycast.transform.forward * this.maxRaycast), Color.green);
        }
    }

    public virtual void SetStopCar(bool triggerStop)
    {
        if (this.ctrl.Agent == null) return;
        this.ctrl.Agent.isStopped = triggerStop;
    }


}
