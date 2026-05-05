using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CarController : TuyenMonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Rigidbody rbCar;
    [SerializeField] protected Transform[] rayPoints;
    [SerializeField] protected LayerMask drivable;//Be mat xe co the chay

    [Header("Suspention Settings")]
    [SerializeField] protected float springStiffness;//Do cung lo xo
    [SerializeField] protected float damperStiffness;
    [SerializeField] protected float restLength;//Chieu dai nghi
    [SerializeField] protected float springTravel;//Nen gian toi da
    [SerializeField] protected float wheelRadius;//Ban kinh ban xe


    [Header("Input")]
    private CarControls playerInputSystem;
    [SerializeField] protected Vector2 move = Vector2.zero;
    [SerializeField] protected Vector2 carRotation = Vector2.zero;
    [SerializeField] protected Vector2 _straight = Vector2.zero;
    [SerializeField] protected float speedCar = 5;

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
        Debug.Log(move);
        Drive(move.y);
        Turn(move.x);
    }
    private void FixedUpdate()
    {
        Suspension();
    }

    private void OnEnable()
    {
        playerInputSystem.Enable();

    }

    private void OnDisable()
    {
        playerInputSystem.Disable();
    }

    private void LoadRayPoint()
    {

    }
    private void Suspension()
    {
        foreach (Transform rayPoint in rayPoints)
        {
            RaycastHit hit;
            float maxLength = restLength + springTravel;

            if (Physics.Raycast(rayPoint.position, -rayPoint.up, out hit, maxLength + wheelRadius, drivable))
            {
                float currentSpringLenght = hit.distance - wheelRadius;
                float springCompression = (restLength - currentSpringLenght) / springTravel;

                float springVelocity = Vector3.Dot(rbCar.GetPointVelocity(rayPoint.position), rayPoint.up);
                float damForce = damperStiffness * springVelocity;

                float springForce = springStiffness * springCompression;

                float netForce = springForce- damForce;

                rbCar.AddForceAtPosition(netForce * rayPoint.up, rayPoint.position);

                Debug.DrawLine(rayPoint.position,hit.point, Color.red);
            }
            else
            {
                Debug.DrawLine(rayPoint.position,rayPoint.position+(wheelRadius + maxLength)* -rayPoint.up, Color.green);
            }
        }

    }
    private void Drive(float straightCar)
    {
        ;
        //rbCar.AddRelativeForce(_straight.x, speedCar);

    }

    private void Turn(float turnRotation)
    {
        carRotation.y = carRotation.y + turnRotation;
        transform.localEulerAngles = carRotation;
    }
}
