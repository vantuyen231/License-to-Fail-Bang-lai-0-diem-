using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


public class CarNPCRaycast : TuyenMonoBehaviour
{
    [SerializeField] protected CarNPCMoving carNPCMoving;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarNPCMoving();
    }

    protected virtual void LoadCarNPCMoving()
    {
        if(carNPCMoving != null) return;
        carNPCMoving = GetComponentInParent<CarNPCMoving>();
        Debug.Log(transform.name + ": LoadCarNPCMoving", gameObject);
    }

    public virtual void RaycastCar()
    {
        //if (raycast == null) return;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, carNPCMoving.MaxRaycast, carNPCMoving.ObstacleLayer))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            //Debug.Log(hit.collider.name);
            carNPCMoving.SetStopByRaycast(true);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + (transform.forward * carNPCMoving.MaxRaycast), Color.green);
            carNPCMoving.SetStopByRaycast(false);
        }
    }
}
