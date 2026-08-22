using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceCarCtrl : PoolObj
{
    [SerializeField] protected PoliceCarMoving policeMoving;
    [SerializeField] protected NavMeshAgent agent;

    [SerializeField] protected float distancePlayer = 0f;

    [Header("Unstuck System")]
    [SerializeField] protected float stuckSpeedThreshold = 0.5f;
    [SerializeField] protected float stuckTimeLimit = 1.5f;     
    [SerializeField] protected float reverseDuration = 1.2f;

    [SerializeField] private float stuckTimer = 0f;
    [SerializeField] private float reverseTimer = 0f;
    [SerializeField] private bool isReversing = false;
    [SerializeField] private float lastTurnDirection = 1f;

    protected override void Start()
    {
        if (this.agent != null)
        {
            this.agent.updatePosition = false;
            this.agent.updateRotation = false;
        }

    }

    protected virtual void Update()
    {
        if (this.policeMoving == null || this.agent == null) return;

        this.LoadTargetPlayer();
        this.AIDriver();

    }

    #region(Load Components)
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoliceMoving();
        this.LoadNavMeshAgent();
    }

    protected virtual void LoadPoliceMoving()
    {
        if (policeMoving != null) return;
        policeMoving = GetComponentInChildren<PoliceCarMoving>();
        Debug.Log(transform.name + ": LoadPoliceMoving", gameObject);
    }

    protected virtual void LoadNavMeshAgent()
    {
        if (agent != null) return;
        agent = GetComponentInChildren<NavMeshAgent>();
        Debug.Log(transform.name + ": LoadNavMeshAgent", gameObject);
    }
    #endregion

    protected virtual void LoadTargetPlayer()
    {
        if (this.policeMoving.CarM != null)
        {
            this.agent.SetDestination(this.policeMoving.CarM.transform.position);
        }
    }

    protected virtual void AIDriver()
    {
        if (!this.agent.hasPath)
        {
            this.policeMoving.SetInputs(0f, 0f);
            return;
        }

        if (this.isReversing)
        {
            this.reverseTimer -= Time.deltaTime;

            this.policeMoving.SetInputs(-1f, -this.lastTurnDirection);

            if (this.reverseTimer <= 0f)
            {
                this.isReversing = false;
                this.stuckTimer = 0f;
            }
            return;
        }

        this.agent.nextPosition = transform.position;

        Vector3 desiredVelocity = this.agent.desiredVelocity;

        if (desiredVelocity.sqrMagnitude > 0.1f)
        {
            Vector3 localDesired = transform.InverseTransformDirection(desiredVelocity.normalized);

            distancePlayer = Vector3.Distance(transform.position, this.policeMoving.CarM.transform.position);

            float angleToTarget = Vector3.Angle(transform.forward, desiredVelocity);

            float turnAmount = localDesired.x;
            float forwardAmount = 0;
            if (distancePlayer > 5f || angleToTarget > 45f)
            {
                forwardAmount = 1f;

                if (localDesired.z < 0)
                {
                    turnAmount = localDesired.x >= 0 ? 1f : -1f;
                }
            }
            else
            {
                if (localDesired.z > 0)
                {
                    forwardAmount = 1f;
                }
                else
                {
                    forwardAmount = -0.5f;
                }
            }
            this.policeMoving.SetInputs(forwardAmount, turnAmount);

            float currentSpeed = this.policeMoving.Rb != null ? this.policeMoving.Rb.velocity.magnitude : 0f;

            if (Mathf.Abs(forwardAmount) > 0.1f && currentSpeed < this.stuckSpeedThreshold)
            {
                this.stuckTimer += Time.deltaTime;
                if (this.stuckTimer >= this.stuckTimeLimit)
                {
                    this.isReversing = true;
                    this.reverseTimer = this.reverseDuration;
                    this.lastTurnDirection = turnAmount >= 0 ? 1f : -1f;
                }
            }
            else
            {
                this.stuckTimer = 0f; 
            }
        }
        else
        {
            this.policeMoving.SetInputs(0f, 0f);
            this.stuckTimer = 0f;
        }
    }



    public override string GetName()
    {
        return "PoliceCar";
    }

}
