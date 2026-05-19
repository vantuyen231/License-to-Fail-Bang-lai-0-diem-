using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontBumpCtrl : MonoBehaviour
{
    [SerializeField] protected Rigidbody rbCar;
    [SerializeField] protected CarController carController;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    NPCRagdoll npcRagdoll = collision.gameObject.GetComponentInParent<NPCRagdoll>();

    //    if (npcRagdoll != null)
    //    {
    //        npcRagdoll.EnableRagdoll();



    //        Vector3 pushDirection = rbCar.velocity.normalized;

    //        float forceMultiplier = 4f;
    //        float pushForceMagnitude = CarController.CarVelocity * forceMultiplier;

    //        Vector3 finalPushForce = pushDirection * pushForceMagnitude;

    //        foreach (Rigidbody npcRb in npcRagdoll.npcRigidbodies)
    //        {
    //            npcRb.AddForce(finalPushForce, ForceMode.Impulse);
    //        }

    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        NPCRagdoll npcRagdoll = other.gameObject.GetComponentInParent<NPCRagdoll>();

        if (npcRagdoll != null)
        {
            npcRagdoll.EnableRagdoll();

            Vector3 pushDirection = rbCar.velocity.normalized;
            if (pushDirection == Vector3.zero) pushDirection = transform.forward;

            float forceMultiplier = 1f;

            float pushForceMagnitude = carController.CarVeclocity * forceMultiplier;

            Vector3 finalPushForce = pushDirection * pushForceMagnitude;

            foreach (Rigidbody npcRb in npcRagdoll.npcRigidbodies)
            {
                if (npcRb != null)
                {
                    npcRb.velocity = Vector3.zero;
                    npcRb.AddForce(finalPushForce, ForceMode.Impulse);
                }
            }
        }
    }
}
