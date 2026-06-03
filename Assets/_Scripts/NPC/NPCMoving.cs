using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMoving : MonoBehaviour
{
    [Header("List Point NPC choices")]
    [SerializeField] protected List<PointPath> localOptions = new List<PointPath>();
    [SerializeField] protected List<PointPath> crossOptions = new List<PointPath>();
    [SerializeField] protected PointPath pointToGo;


    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected int crossRoadChance = 70;
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
        
        localOptions = this.pointToGo.GetLocalPoints();
        crossOptions = this.pointToGo.GetNextCrossRoadPoints();


    }

    protected virtual void ChoiceNextPoint()
    {
        bool hasLocal = localOptions != null && localOptions.Count > 0;
        bool hasCross = crossOptions != null && crossOptions.Count > 0;

        if (!hasLocal && !hasCross)
        {
            this.pointToGo = null;
            return;
        }

        PointPath selectedPoint = null;

        int numRoll = Random.Range(0, 100);

        if (numRoll <= crossRoadChance)
        {
            if (hasCross)
            {
                selectedPoint = crossOptions[Random.Range(0, crossOptions.Count)];
            }
            else if (hasLocal)
            {
                selectedPoint = localOptions[Random.Range(0, localOptions.Count)];
            }
        }
        else
        {
            if (hasLocal)
            {
                selectedPoint = localOptions[Random.Range(0, localOptions.Count)];
            }
            else if (hasCross)
            {
                selectedPoint = crossOptions[Random.Range(0, crossOptions.Count)];
            }
        }
        pointToGo = selectedPoint;
        agent.SetDestination(pointToGo.transform.position);
        //int choicesPoint = Random.Range(0,allAvailableChoices.Count);
        //this.pointToGo = allAvailableChoices[choicesPoint];
        //agent.SetDestination(pointToGo.transform.position);
    }
}
