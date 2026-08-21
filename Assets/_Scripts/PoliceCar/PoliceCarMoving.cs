using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarMoving : TuyenMonoBehaviour
{
    [SerializeField] protected float moveSpeed = 25f;
    [SerializeField] protected float turnSpeed = 120f;

    [SerializeField] protected float motorPower = 2000f;   
    [SerializeField] protected float brakePower = 4000f;   
    [SerializeField] protected float maxSteerAngle = 35f;

    [SerializeField] protected Transform centerOfMass;
    [SerializeField] protected CarManager carPlayer;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float currentForwardInput;
    [SerializeField] protected float currentTurnInput;

    [Header("WheelCollider")]
    [SerializeField] protected List<WheelCollider> wheelCollidersCtrl = new List<WheelCollider>();
    [SerializeField] protected List<WheelTransformCtrl> wheelTransformsCtrl = new List<WheelTransformCtrl>();

    private void FixedUpdate()
    {
        this.ApplyMotor();
        this.ApplySteering();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWheelColliders();
        this.LoadWheelTransform();
        this.LoadRigidbody();
        this.LoadTransform();
    }

    protected virtual void LoadWheelColliders()
    {
        if (this.wheelCollidersCtrl.Count > 0) return;
        this.wheelCollidersCtrl.AddRange(GetComponentsInChildren<WheelCollider>());
    }

    protected virtual void LoadWheelTransform()
    {
        if (this.wheelTransformsCtrl.Count > 0) return;
        this.wheelTransformsCtrl.AddRange(GetComponentsInChildren<WheelTransformCtrl>());

    }

    protected virtual void LoadRigidbody()
    {
        if(rb != null) return;
        rb = GetComponent<Rigidbody>();
        Debug.Log(transform.name + ": LoadRigidbody", gameObject);
    }

    protected virtual void LoadTransform()
    {
        if (centerOfMass != null) return;
        centerOfMass = GetComponent<Transform>();
        Debug.Log(transform.name + ": LoadTransform", gameObject);
    }

    public virtual void SetInputs(float forward, float turn)
    {
        this.currentForwardInput = Mathf.Clamp(forward, -1f, 1f);
        this.currentTurnInput = Mathf.Clamp(turn, -1f, 1f);
    }

    protected virtual void ApplyMotor()
    {
        //Vector3 forceDirection = transform.forward * (this.currentForwardInput * this.moveSpeed);
        //this.rb.AddForce(forceDirection, ForceMode.Acceleration);
        if (Mathf.Abs(this.currentForwardInput)>0.05f)
        {
            wheelCollidersCtrl[0].brakeTorque = 0;
            wheelCollidersCtrl[1].brakeTorque = 0; 
            wheelCollidersCtrl[2].brakeTorque = 0;
            wheelCollidersCtrl[3].brakeTorque = 0;
            float torque = this.currentForwardInput * this.motorPower;
            wheelCollidersCtrl[0].motorTorque = torque;
            wheelCollidersCtrl[1].motorTorque = torque;

        }
        else
        {
            wheelCollidersCtrl[0].motorTorque = 0;
            wheelCollidersCtrl[1].motorTorque = 0;
            wheelCollidersCtrl[0].brakeTorque = brakePower;
            wheelCollidersCtrl[1].brakeTorque = brakePower;
            wheelCollidersCtrl[2].brakeTorque = brakePower;
            wheelCollidersCtrl[3].brakeTorque = brakePower;

        }

    }

    protected virtual void ApplySteering()
    {
        if (Mathf.Abs(this.currentForwardInput) > 0.05f)
        {
            wheelCollidersCtrl[2].steerAngle = this.currentTurnInput * this.turnSpeed;
            wheelCollidersCtrl[3].steerAngle = this.currentTurnInput * this.turnSpeed;
        }
    }
    protected virtual void OnEnable()
    {
        PlayerSpawner.OnPlayerSpawned += HandlePlayerSpawned;
    }

    protected virtual void OnDisable()
    {
        PlayerSpawner.OnPlayerSpawned -= HandlePlayerSpawned;
    }

    private void HandlePlayerSpawned(CarManager player)
    {
        this.carPlayer = player;
        Debug.Log(transform.name + ": da Target tu Event", gameObject);
    }


}
