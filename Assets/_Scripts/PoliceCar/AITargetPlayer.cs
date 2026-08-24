using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITargetPlayer : TuyenMonoBehaviour
{
    [Header("LoadComponents")]
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected PoliceCarMoving policeCarMoving;


    [Header("AgetValue")]
    [SerializeField] protected float distancePlayer = 0f;
    protected Vector3 localDesired;
    protected float rawSpeedPolice;
    [SerializeField] protected float speedPolice;
    [SerializeField] protected float speedLimit = 50f;
    [SerializeField] protected float stuckSpeedThreshold = 0.3f;

    [Header("AIVelocity")]
    [SerializeField] protected float forwardAmount = 0f;
    [SerializeField] protected float turnAmount = 0f;
    [SerializeField] protected Vector3 forceAgent;
    [SerializeField] protected float angleToTarget;

    [Header("AntiStuck")]
    [SerializeField] protected float currentStuckTime = 0f;
    [SerializeField] protected float stuckTimeLimit = 1f;
    [SerializeField] protected bool isReversing = false;
    [SerializeField] protected float reverseDuration = 3f;
    [SerializeField] protected float reverseTimer = 0f;
    [SerializeField] protected float lastTurnDirection = 1f;
    [SerializeField] protected float brakeBufferTimer = 0f;

    [Header("Optimization Settings")]
    [SerializeField] protected float nearDistance = 15f;    
    [SerializeField] protected float nearInterval = 0.1f;    
    [SerializeField] protected float farInterval = 0.3f;



    protected override void Start()
    {
        base.Start();
        this.OffUpdateAgentRoad();
    }

    private void FixedUpdate()
    {
        //this.TargetPlayer();
        this.AIFollowPlayer();
    }

    #region(LoadComponents)
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAgent();
        this.LoadMoving();
    }

    protected virtual void LoadAgent()
    {
        if (agent != null) return;
        agent = GetComponent<NavMeshAgent>();
        Debug.Log(transform.name + ": LoadNavMeshAgent", gameObject);
    }

    protected virtual void LoadMoving()
    {
        if (policeCarMoving != null) return;
        policeCarMoving = GetComponent<PoliceCarMoving>();
        Debug.Log(transform.name + ": LoadMoving", gameObject);
    }

    #endregion

    private Coroutine targetUpdateCoroutine;

    protected virtual void OnEnable()
    {
        this.targetUpdateCoroutine = StartCoroutine(this.TargetPlayerRoutine());
    }

    protected virtual void OnDisable()
    {
        if (this.targetUpdateCoroutine != null)
        {
            StopCoroutine(this.targetUpdateCoroutine);
        }
    }

    protected virtual IEnumerator TargetPlayerRoutine()
    {
        while (true)
        {
            float delay = this.farInterval;
            if (this.agent != null && this.policeCarMoving != null && this.policeCarMoving.CarM != null)
            {

                this.TargetPlayer();
                distancePlayer = Vector3.Distance(transform.position, this.policeCarMoving.CarM.transform.position);
                if (distancePlayer <= this.nearDistance)
                {
                    delay = this.nearInterval;
                }
            }

            yield return new WaitForSeconds(delay);
        }
    }



    protected virtual void TargetPlayer()
    {
        if(agent == null) return;
        this.agent.SetDestination(this.policeCarMoving.CarM.transform.position);
    }

    protected virtual void OffUpdateAgentRoad()
    {
        if (agent == null) return;
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    protected virtual void AIFollowPlayer()
    {
        this.agent.nextPosition = transform.position;

        if (!agent.hasPath)
        {
            this.policeCarMoving.SetInputs(0f,0f);
            return;
        }

        forceAgent = agent.desiredVelocity;
        //distancePlayer = Vector3.Distance(transform.position, policeCarMoving.CarM.transform.position);


        if (forceAgent.sqrMagnitude < 0.1f || distancePlayer < 2f)
        {
            this.policeCarMoving.SetInputs(0f, 0f);
            this.currentStuckTime = 0f;
            return;
        }


        localDesired = transform.InverseTransformDirection(forceAgent.normalized);
        angleToTarget = Vector3.Angle(transform.forward, forceAgent);

        rawSpeedPolice = this.policeCarMoving.Rb != null ? this.policeCarMoving.Rb.velocity.magnitude : 0f;
        speedPolice = rawSpeedPolice * 3.6f; 
        
        forwardAmount = localDesired.z;
        turnAmount = localDesired.x;

        if (isReversing)
        {
            this.AnitStuck();
            return;
        }
        this.CheckStuck();
        this.AITurnBack();

        if (speedPolice > speedLimit)
        {
            forwardAmount =0f;
        }
        policeCarMoving.SetInputs(forwardAmount, turnAmount);
    }

    protected virtual void CheckStuck()
    {
        if (Mathf.Abs(forwardAmount) > 0.1f && rawSpeedPolice < stuckSpeedThreshold)
        {
            currentStuckTime += Time.deltaTime;
            if (currentStuckTime >= stuckTimeLimit)
            {
                isReversing = true;
                this.reverseTimer = reverseDuration;
                this.brakeBufferTimer = 0.2f;
                lastTurnDirection = turnAmount >= 0 ? 1f : -1f;
            }
        }
        else
        {
            currentStuckTime = 0f;
        }
    }

    protected virtual void AnitStuck()
    {
        if (isReversing)
        {
            reverseTimer -= Time.deltaTime;

            if (reverseTimer > 0f)
            {
                this.policeCarMoving.SetInputs(-1f, -lastTurnDirection);
                return;
            }

            if (brakeBufferTimer > 0f)
            {
                brakeBufferTimer -= Time.deltaTime;
                this.policeCarMoving.SetInputs(0f, 0f);
                if (this.policeCarMoving.Rb != null)
                {
                    this.policeCarMoving.Rb.velocity = Vector3.Lerp(this.policeCarMoving.Rb.velocity, Vector3.zero, Time.deltaTime * 10f);
                    this.policeCarMoving.Rb.angularVelocity = Vector3.zero;
                }
                return;
            }
            isReversing = false;
            currentStuckTime = 0f;
            this.agent.ResetPath();
        }
    }

    protected virtual void AITurnBack()
    {
        if (distancePlayer > 6 && angleToTarget > 50)
        {
            forwardAmount = 1f;
            if (localDesired.z < 0)
            {
                turnAmount = localDesired.x >= 0 ? 1 : -1;
            }
            else
            {
                this.turnAmount = this.localDesired.x;
            }
        }
        else
        {
            forwardAmount = localDesired.z >= 0f ? 1f : -0.5f;
            this.turnAmount = this.localDesired.x;
        }
    }


}
