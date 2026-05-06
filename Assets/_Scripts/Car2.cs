using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car2 : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Rigidbody rbCar;
    [SerializeField] protected Transform[] rayPoints;
    [SerializeField] protected LayerMask drivable;//Be mat xe co the chay
    //[SerializeField] protected Transform accelerationPoint;

    [Header("Suspention Settings")]
    [SerializeField] protected float springStiffness;//Do cung lo xo
    //[SerializeField] protected float damperStiffness;
    [SerializeField] protected float restLength;//Chieu dai nghi
    [SerializeField] protected float springTravel;//Nen gian toi da
    [SerializeField] protected float wheelRadius;//Ban kinh ban xe

    private void Start()
    {
        rbCar = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Suspension();
    }

    private void Suspension()
    {
        foreach (Transform rayPoint in rayPoints)
        {
            RaycastHit hit;
            float maxLength = restLength + springTravel;

            if(Physics.Raycast(rayPoint.position, -rayPoint.up, out hit, maxLength + wheelRadius, drivable))
            {
                float currentSpringLenght = hit.distance - wheelRadius;
                float springCompression = (restLength - currentSpringLenght) / springTravel;

                float springForce = springStiffness * springCompression;

                rbCar.AddForceAtPosition(springForce * rayPoint.up, rayPoint.position);
                Debug.DrawLine(rayPoint.position, hit.point, Color.red);
            }
            else
            {
                Debug.DrawLine(rayPoint.position, rayPoint.position + (wheelRadius + maxLength)*-rayPoint.up, Color.green);
            }
        }
    }
}
