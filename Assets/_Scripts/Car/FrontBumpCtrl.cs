using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontBumpCtrl : TuyenMonoBehaviour
{
    [SerializeField] protected CarController carController;
    [SerializeField] protected float forceMultiplier = 0.5f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCarCtrll();
    }
    protected virtual void LoadCarCtrll()
    {
        if(carController != null) return;
        carController = transform.parent.GetComponent<CarController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        NPCRagdoll npcRagdoll = other.gameObject.GetComponentInParent<NPCRagdoll>();

        if (npcRagdoll != null)
        {
            npcRagdoll.EnableRagdoll();

            Vector3 pushDirection = carController.RbCar.velocity.normalized;
            if (pushDirection == Vector3.zero) pushDirection = transform.forward;

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
