using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarMoving : TuyenMonoBehaviour
{
    [SerializeField] protected float moveSpeed = 25f;
    [SerializeField] protected float turnSpeed = 120f;
    [SerializeField] protected Transform centerOfMass;
    [SerializeField] protected CarManager carPlayer;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float currentForwardInput;
    [SerializeField] protected float currentTurnInput;

    private void FixedUpdate()
    {
        this.ApplyMotor();
        this.ApplySteering();
    }


    public virtual void SetInputs(float forward, float turn)
    {
        this.currentForwardInput = Mathf.Clamp(forward, -1f, 1f);
        this.currentTurnInput = Mathf.Clamp(turn, -1f, 1f);
    }

    protected virtual void ApplyMotor()
    {
        Vector3 forceDirection = transform.forward * (this.currentForwardInput * this.moveSpeed);
        this.rb.AddForce(forceDirection, ForceMode.Acceleration);
    }

    protected virtual void ApplySteering()
    {
        if (Mathf.Abs(this.currentForwardInput) > 0.05f)
        {
            float turn = this.currentTurnInput * this.turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            this.rb.MoveRotation(this.rb.rotation * turnRotation);
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
