using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CarController : TuyenMonoBehaviour
{
    [Header("WheelCollider")]
    [SerializeField] protected WheelCollider FrontRightWheelCollider;
    [SerializeField] protected WheelCollider FrontLeftWheelCollider;
    [SerializeField] protected WheelCollider BackRightWheelCollider;
    [SerializeField] protected WheelCollider BackLeftWheelCollider;

    [SerializeField] protected Transform FrontLeft;
    [SerializeField] protected Transform FrontRight;
    [SerializeField] protected Transform BackRight;
    [SerializeField] protected Transform BackLeft;



    [Header("References")]
    [SerializeField] protected Rigidbody rbCar;
    [SerializeField] protected Transform carCentreOfMess;

    [Header("Drive Systems")]
    [SerializeField] protected float motorForce = 200f;
    [SerializeField] protected float steerWheel = 30f;
    [SerializeField] protected float brakeForce = 100f;


    [Header("Camera Follow")]
    [SerializeField] protected Transform lookAtPoint;
    [SerializeField] protected Vector3 targetLookAt;
    [SerializeField] protected float maxTurn = 2f;
    [SerializeField] protected float lookAtShiftSpeed = 2f;      
    [SerializeField] protected float returnSpeed = 0.02f;

    [Header("Input")]
    private CarControls playerInputSystem;
    [SerializeField] protected Vector2 move = Vector2.zero;
    [SerializeField] protected float moveInput = 0;
    [SerializeField] protected float steerInput = 0;

    //[Header("Car Settings")]
    //[SerializeField] protected float acceleration = 25f;


    //[SerializeField] private Vector3 currentCarLocalVecocity = Vector3.zero;
    //[SerializeField] private float carVelocityRatio = 0;

    protected override void Awake()
    {
        playerInputSystem = new CarControls();
        rbCar = GetComponent<Rigidbody>();
        rbCar.centerOfMass = carCentreOfMess.position;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }


    private void Update()
    {
        move = playerInputSystem.Player.Move.ReadValue<Vector2>();
        moveInput = move.y;
        steerInput = move.x;
    }
    private void FixedUpdate()
    {
        CarForce();
        UpdateWheel();
        Steering();
        TurnCam();
    }

    private void OnEnable()
    {
        playerInputSystem.Enable();
        playerInputSystem.Player.Brake.performed += ctx => { ApplyBrake(); };
    }

    private void OnDisable()
    {
        playerInputSystem.Disable();
        
    }

    private void CarForce()
    {
        BackLeftWheelCollider.motorTorque = motorForce * moveInput;
        BackRightWheelCollider.motorTorque = motorForce * moveInput;
    }

    private void Steering()
    {
        FrontLeftWheelCollider.steerAngle = steerWheel * steerInput;
        FrontRightWheelCollider.steerAngle = steerWheel * steerInput;
    }

    private void ApplyBrake()
    {
        FrontLeftWheelCollider.brakeTorque = brakeForce;
        FrontRightWheelCollider.brakeTorque = brakeForce;
    }

    private void TurnCam()
    {
        targetLookAt = lookAtPoint.localPosition;
        targetLookAt.x += lookAtShiftSpeed *  steerInput * Time.deltaTime;
        if(steerInput == 0)
        {
            targetLookAt.x = Mathf.MoveTowards(targetLookAt.x, 0, returnSpeed);
        }
        targetLookAt.x = Mathf.Clamp(targetLookAt.x,-maxTurn,maxTurn);
        lookAtPoint.localPosition = targetLookAt;

    }

    private void UpdateWheel()
    {
        RotationWheel(FrontLeftWheelCollider,FrontLeft);
        RotationWheel(FrontRightWheelCollider,FrontRight);
        RotationWheel(BackLeftWheelCollider, BackLeft);
        RotationWheel(BackRightWheelCollider, BackRight);

    }

    private void RotationWheel(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }

    private void SimulatorRollBodyCar()
    {

    }

}
