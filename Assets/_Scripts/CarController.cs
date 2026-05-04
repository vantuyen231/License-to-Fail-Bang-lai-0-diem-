using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CarController : TuyenMonoBehaviour
{
    private CarControls playerInputSystem;
    [SerializeField] protected Rigidbody rbCar;
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

    private void OnEnable()
    {
        playerInputSystem.Enable();

    }

    private void OnDisable()
    {
        playerInputSystem.Disable();
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
