using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CarController : TuyenMonoBehaviour
{
    [SerializeField] protected CarManager carManager;

    [Header("WheelCollider")]
    [SerializeField] protected List<WheelCollider> wheelCollidersCtrl = new List<WheelCollider>();
    [SerializeField] protected List<WheelTransformCtrl> wheelTransformsCtrl = new List<WheelTransformCtrl>();


    [Header("References")]
    [SerializeField] protected Rigidbody rbCar;
    [SerializeField] protected Transform carCentreOfMess;
    public Rigidbody RbCar => rbCar;


    [Header("Roll Body Car")]
    [SerializeField] protected BodyCar bodyCar;
    [SerializeField] protected float rollAngle = 5f;
    [SerializeField] protected float yawAngle = 3f;

    [Header("Drive Systems")]
    [SerializeField] protected float motorForce = 200f;
    [SerializeField] protected float steerWheel = 30f;
    [SerializeField] protected float brakeForce = 50f;
    [SerializeField] protected float carVelocity;
    [SerializeField] protected int  playerSpeed = 0;
    public float CarVeclocity => carVelocity;
    public int PlayerSpeed => playerSpeed;



    [Header("Camera Follow")]
    [SerializeField] protected Transform lookAtPoint;
    [SerializeField] protected Vector3 targetLookAt;
    [SerializeField] protected float maxTurn = 2f;
    [SerializeField] protected float lookAtShiftSpeed = 2f;
    [SerializeField] protected float returnSpeed = 2f;

    [Header("Input")]
    private CarControls playerInputSystem;
    [SerializeField] protected Vector2 move = Vector2.zero;
    [SerializeField] protected float moveInput = 0;
    [SerializeField] protected float steerInput = 0;

    protected override void Start()
    {
        base.Start();
        this.LoadComponents();
    }

    #region Loand Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
        this.LoadWheelColliders();
        this.LoadPointCamLook();
        this.LoadWheelTransform();
        this.LoadCentreMass();
        this.LoadBodyCar();
        this.LoadCarManager();
        this.LoadStatusCar();
    }

    protected virtual void LoadRigidbody()
    {
        if(rbCar != null) return;
        rbCar = GetComponent<Rigidbody>();
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

    protected virtual void LoadPointCamLook()
    {
        if (this.lookAtPoint != null) return;
        CamLookAtPoint scriptLookAtPoint = GetComponentInChildren<CamLookAtPoint>();
        this.lookAtPoint = scriptLookAtPoint.transform;
    }

    protected virtual void LoadCentreMass()
    {
        if (this.carCentreOfMess != null) return;
        CarCentreOfMass scriptMess = GetComponentInChildren<CarCentreOfMass>();
        this.carCentreOfMess = scriptMess.transform;
    }

    protected virtual void LoadBodyCar()
    {
        if (this.bodyCar != null) return;
        this.bodyCar = GetComponentInChildren<BodyCar>();
    }

    protected virtual void LoadCarManager()
    {
        if (carManager != null) return;
        carManager = GetComponent<CarManager>();
        Debug.Log(transform.name + ": LoadCarManager", gameObject);
    }

    protected virtual void LoadStatusCar()
    {
        if(bodyCar ==  null || this.bodyCar.CarData == null) return;

        CarPlayerDataSO dataCar = this.carManager.PlayerSpawner.PlayerDataSO;

        this.rollAngle = dataCar.rollAngel;
        this.yawAngle = dataCar.yallAngel;
        this.motorForce = dataCar.motorForce;
        this.steerWheel = dataCar.steerWheel;
        this.brakeForce = dataCar.brakeForce;
    }
    #endregion

    #region inputSystem, Update
    protected override void Awake()
    {
        base.Awake();
        playerInputSystem = new CarControls();
        rbCar.centerOfMass = carCentreOfMess.localPosition;
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
        CarSpeed();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space!");
        }
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

        wheelCollidersCtrl[0].brakeTorque = brakeForce;
        wheelCollidersCtrl[1].brakeTorque = brakeForce;
    }

    private void ReleaseBrake()
    {
        wheelCollidersCtrl[2].brakeTorque = 0;
        wheelCollidersCtrl[3].brakeTorque = 0;

        wheelCollidersCtrl[0].brakeTorque = 0;
        wheelCollidersCtrl[1].brakeTorque = 0;
    }
    #endregion

    private void TurnCam()
    {
        if (carVelocity <= 0.1 && Mathf.Approximately(targetLookAt.x,0)) { return; }
        targetLookAt = lookAtPoint.localPosition;
        if (steerInput == 0)
        {
            targetLookAt.x = Mathf.MoveTowards(targetLookAt.x, 0, returnSpeed*Time.deltaTime);
        }
        else
        {
            targetLookAt.x += lookAtShiftSpeed * steerInput * Time.deltaTime;

        }
        targetLookAt.x = Mathf.Clamp(targetLookAt.x, -maxTurn, maxTurn);

        lookAtPoint.localPosition = targetLookAt;

    }
    #region wheelCollider
    private void UpdateWheel()
    {
        for (int i = 0; i < wheelCollidersCtrl.Count; i++)
        {
            RotationWheel(wheelCollidersCtrl[i], wheelTransformsCtrl[i].transform);
        }

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
        if (moveInput == 0 && Mathf.Approximately(targetLookAt.x, 0)) { return; }
        float targetRollAngle = steerInput * rollAngle;
        float targetTurnAngle = steerInput * yawAngle;
        Quaternion targetRot = Quaternion.Euler(0, targetTurnAngle, targetRollAngle);
        bodyCar.transform.localRotation = Quaternion.Lerp(bodyCar.transform.localRotation, targetRot, Time.deltaTime * 5f);
    }

    private void CarSpeed()
    {
        carVelocity = rbCar.velocity.magnitude;
        float rawSpeed = carVelocity * 3.6f;
        if (rawSpeed < 0.01f)
        {
            playerSpeed = 0;
        }
        else
        {
            playerSpeed = (int)rawSpeed;
        }

        GameManager.Instance.UpdateVelocity(playerSpeed);
    }


}
