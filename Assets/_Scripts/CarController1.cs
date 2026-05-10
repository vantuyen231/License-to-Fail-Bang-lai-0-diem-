using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CarController1 : TuyenMonoBehaviour
{
    [Header("WheelCollider")]
    [SerializeField] protected List<WheelCollider> wheelCollidersCtrl = new List<WheelCollider>();

    [SerializeField] protected List <WheelTransformCtrl> wheelTransformsCtrl = new List<WheelTransformCtrl>();
    //[SerializeField] protected Transform FrontLeft;
    //[SerializeField] protected Transform FrontRight;
    //[SerializeField] protected Transform BackRight;
    //[SerializeField] protected Transform BackLeft;



    [Header("References")]
    [SerializeField] protected Rigidbody rbCar;
    [SerializeField] protected Transform carCentreOfMess;

    [Header("Roll Body Car")]
    [SerializeField] protected Transform bodyCar;
    [SerializeField] protected float rollAngle = 5f;
    [SerializeField] protected float yawAngle = 3f;

    [Header("Drive Systems")]
    [SerializeField] protected float motorForce = 200f;
    [SerializeField] protected float steerWheel = 30f;
    [SerializeField] protected float brakeForce = 1000f;


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

    #region Loand Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWheelColliders();
        this.LoadPointCamLook();
        this.LoadWheelTransform();
    }

    protected virtual void LoadWheelColliders()
    {
        if (this.wheelCollidersCtrl.Count > 0) return;
            
        this.wheelCollidersCtrl.AddRange(GetComponentsInChildren<WheelCollider>());
        
    
        Debug.Log(transform.name + ": Loaded " + wheelCollidersCtrl.Count + " WheelColliders");
    }

    protected virtual void LoadWheelTransform()
    {
        //Transform carObj = transform.Find("sedan");
        //if (carObj == null) return;
        ////if(wheelCollidersCtrl != null) return;
        //foreach (Transform child in carObj)
        //{

        //    WheelTransformCtrl wheelTransform = child.GetComponentInChildren<WheelTransformCtrl>();
        //    this.wheelTransformsCtrl.Add(wheelTransform);
        //}
        if (this.wheelTransformsCtrl.Count > 0) return;
        Transform sedanObj = transform.Find("sedan");

        if (sedanObj != null)
        {
            foreach (Transform child in sedanObj)
            {
                WheelTransformCtrl wheel = child.GetComponent<WheelTransformCtrl>();
                if (wheel != null)
                {
                    this.wheelTransformsCtrl.Add(wheel);
                }
            }
        }
    }

    protected virtual void LoadPointCamLook()
    {
        if (lookAtPoint != null) return;
        this.lookAtPoint = transform.Find("CamLookAtPoint").GetComponent<Transform>();
        Debug.Log(transform.name + ": Loaded " + lookAtPoint + " LoadPointCamLook");
    }

    #endregion

    #region inputSystem, Update
    protected override void Awake()
    {
        base.Awake();
        playerInputSystem = new CarControls();
        rbCar = GetComponent<Rigidbody>();
        rbCar.centerOfMass = carCentreOfMess.position;
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
        SimulatorRollBodyCar();
    }

    private void OnEnable()
    {
        playerInputSystem.Enable();
        playerInputSystem.Player.Brake.performed += ctx => { ApplyBrake(); };
        playerInputSystem.Player.Brake.canceled += ctx => { ReleaseBrake(); };
    }

    private void OnDisable()
    {
        playerInputSystem.Disable();
        
    }
    #endregion
    private void CarForce()
    {
        wheelCollidersCtrl[0].motorTorque = motorForce * moveInput;
        wheelCollidersCtrl[1].motorTorque = motorForce * moveInput;
    }

    private void Steering()
    {
        wheelCollidersCtrl[2].steerAngle = steerWheel * steerInput;
        wheelCollidersCtrl[3].steerAngle = steerWheel * steerInput;

    }

    #region Brake
    private void ApplyBrake()
    {
        wheelCollidersCtrl[2].brakeTorque = brakeForce;
        wheelCollidersCtrl[3].brakeTorque = brakeForce;

        wheelCollidersCtrl[0].motorTorque = 0;
        wheelCollidersCtrl[1].motorTorque = 0;
    }

    private void ReleaseBrake()
    {
        wheelCollidersCtrl[2].brakeTorque = 0;
        wheelCollidersCtrl[3].brakeTorque = 0;
    }
    #endregion

    private void TurnCam()
    {
        if(moveInput == 0) { return;}
        targetLookAt = lookAtPoint.localPosition;
        targetLookAt.x += lookAtShiftSpeed *  steerInput * Time.deltaTime;
        if(steerInput == 0)
        {
            targetLookAt.x = Mathf.MoveTowards(targetLookAt.x, 0, returnSpeed);
        }
        targetLookAt.x = Mathf.Clamp(targetLookAt.x,-maxTurn,maxTurn);
        lookAtPoint.localPosition = targetLookAt;

    }
    #region wheelCollider
    private void UpdateWheel()
    {
        RotationWheel(wheelCollidersCtrl[2], wheelTransformsCtrl[2].transform);
        RotationWheel(wheelCollidersCtrl[3], wheelTransformsCtrl[3].transform);
        RotationWheel(wheelCollidersCtrl[0], wheelTransformsCtrl[0].transform);
        RotationWheel(wheelCollidersCtrl[1], wheelTransformsCtrl[1].transform);
        //if (wheelCollidersCtrl.Count != wheelTransformsCtrl.Count) return;

        //for (int i = 0; i < wheelCollidersCtrl.Count; i++)
        //{
        //    RotationWheel(wheelCollidersCtrl[i], wheelTransformsCtrl[i].transform);
        //}
    }

    private void RotationWheel(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }
    #endregion
    private void SimulatorRollBodyCar()
    {
        if(moveInput == 0) {return;}
        float targetRollAngle = steerInput * rollAngle;
        float targetTurnAngle = steerInput * yawAngle;
        Quaternion targetRot = Quaternion.Euler(0, targetTurnAngle, targetRollAngle);
        bodyCar.localRotation = Quaternion.Lerp(bodyCar.localRotation, targetRot, Time.deltaTime * 5f);
    }

}
