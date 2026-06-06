using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

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

    [SerializeField] protected float waitTime = 3f;
    [SerializeField] protected float countDown = 0f;
    [SerializeField] protected bool isWaiting = false;
    [SerializeField] protected int countAction = 2;


    public void Start()
    {
        agent.SetDestination(pointToGo.transform.position);
        this.SetTimeCountDown();
    }

    protected void FixedUpdate()
    {

        if (isWaiting)
        {
            this.TimerSystem();
        }
        else
        {
            this.GoToTarget();
        }
    }

    protected virtual void ChoiceAction()
    {
        int numAction = Random.Range(0, crossOptions.Count);
        Debug.Log(numAction);
        if(numAction == 0)
        {
            this.ContinueJourney();
            Debug.Log("Walk");
        }
        else
        {
            this.Idle();
            Debug.Log("Wait");
        }
    }

    protected virtual void SetTimeCountDown()
    {
        countDown = waitTime;
    }

    protected virtual void TimerSystem()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            isWaiting = false;

            agent.isStopped = false;
            this.ContinueJourney();
        }

    }

    protected virtual void Idle()
    {
        isWaiting = true;
        agent.isStopped = true;
        this.SetTimeCountDown();

    }

    public virtual void GoToTarget()
    {
        if (pointToGo == null || agent == null || !agent.isActiveAndEnabled ) return;

        if (agent.pathPending) return;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            this.ChoiceAction();
            //this.LoadNextPoint();
            //this.ChoiceNextPoint();
        }
    }
    protected virtual void ContinueJourney()
    {
        this.LoadNextPoint();
        this.ChoiceNextPoint();
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

    }
}
