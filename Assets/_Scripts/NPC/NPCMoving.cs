using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMoving : MonoBehaviour
{
    [SerializeField] protected List<PointPath> allAvailableChoices = new List<PointPath>();
    [SerializeField] protected PointPath pointToGo;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Transform nextPoint;
    [SerializeField] protected float targetDistance = 0f;
    [SerializeField] protected float stopDistance =1f;


    public void Start()
    {

    }

    protected void FixedUpdate()
    {
        this.GoToTarget();
    }
    protected virtual void GoToTarget()
    {
        if (pointToGo == null) return;
        Vector3 position = this.pointToGo.transform.position;

        agent.SetDestination(position);
        targetDistance = Vector3.Distance(transform.position, this.pointToGo.transform.position);
        if (targetDistance < stopDistance)
        {
            this.LoadNextPoint();
            this.ChoiceNextPoint();
        }
    }
    protected virtual void LoadNextPoint()
    {
        List<PointPath> localOptions = this.pointToGo.GetLocalPoints();
        List<PointPath> crossOptions = this.pointToGo.GetNextCrossRoadPoints();

        allAvailableChoices.Clear();
        if (localOptions != null) this.allAvailableChoices.AddRange(localOptions);
        if (crossOptions != null) this.allAvailableChoices.AddRange(crossOptions);
    }

    protected virtual void ChoiceNextPoint()
    {
        int choicesPoint = Random.Range(0,allAvailableChoices.Count);
        this.pointToGo = allAvailableChoices[choicesPoint];
        agent.SetDestination(pointToGo.transform.position);
    }
}
