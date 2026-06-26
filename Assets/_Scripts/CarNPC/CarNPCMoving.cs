using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class CarNPCMoving : TuyenMonoBehaviour
{
    [SerializeField] protected LocalPointStreet pointToGo;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected LocalPointStreet nextPoint;



    protected virtual void LateUpdate()
    {
        this.CheckDistanceAndChangePoint();
    }
    protected virtual void MoveToTarget()
    {
        if (agent == null) return;
        Vector3 point = pointToGo.transform.position;

        agent.SetDestination(point);
        Debug.Log("Move");
    }

    protected virtual void LoadNextPoint()
    {
        pointToGo = this.pointToGo.NextPointInStreet;
        Debug.Log("Load");
    }

    protected virtual void CheckDistanceAndChangePoint()
    {
        if (this.agent.remainingDistance <= this.agent.stoppingDistance)
        {
            this.LoadNextPoint();
            this.MoveToTarget();

        }
    }
}
