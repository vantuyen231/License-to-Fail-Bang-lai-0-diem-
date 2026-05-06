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
    //[SerializeField] protected Transform accelerationPoint;
    ////[SerializeField] protected bool isGrounded = false;
    ////[SerializeField] protected int[] wheelsIsGround = new int[4];


    [Header("Input")]
    private CarControls playerInputSystem;
    [SerializeField] protected Vector2 move = Vector2.zero;
    [SerializeField] protected float moveInput = 0;
    [SerializeField] protected float steerInput = 0;

    //[Header("Car Settings")]
    //[SerializeField] protected float acceleration = 25f;
    //[SerializeField] protected float maxSpeed = 100f;
    //[SerializeField] protected float deceleration = 10f;

    //[SerializeField] private Vector3 currentCarLocalVecocity = Vector3.zero;
    //[SerializeField] private float carVelocityRatio = 0;

    protected override void Awake()
    {

        playerInputSystem = new CarControls();
        rbCar = GetComponent<Rigidbody>();
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
        //Debug.Log("move" + moveInput);
        //Debug.Log("steer" + steerInput);

    }
    private void FixedUpdate()
    {
        CarForce();
        UpdateWheel();
    }

    private void OnEnable()
    {
        playerInputSystem.Enable();

    }

    private void OnDisable()
    {
        playerInputSystem.Disable();
    }

    private void CarForce()
    {
        FrontLeftWheelCollider.motorTorque = 100f *moveInput;
        FrontRightWheelCollider.motorTorque = 100f *moveInput;
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

}
