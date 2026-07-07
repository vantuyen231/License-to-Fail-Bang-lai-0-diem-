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
        PlayerScore playerScore = carController.GetComponentInChildren<PlayerScore>();

        CarNPCInfo carNPCInfo = other.gameObject.GetComponentInParent<CarNPCInfo>();
        if(carNPCInfo != null)
        {
            this.HitCarNPCTrigger(carNPCInfo, playerScore);
            return;
        }

        NPCGameInfo npcGameInfo = other.gameObject.GetComponentInParent<NPCGameInfo>(); 
        if(npcGameInfo != null)
        {
            this.HitNPCTrigger(other, npcGameInfo, playerScore);
            return;
        }
    }

    protected virtual void HitNPCTrigger(Collider other, NPCGameInfo npcGameInfo, PlayerScore playerScore)
    {
        if (npcGameInfo == null) return;
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
        HitObjectType type = npcGameInfo.ObjectDataSO.hitObjectType;
        string nameNPC = npcGameInfo.ObjectDataSO.hitObjectName;
        int scoreReward = npcGameInfo.ObjectDataSO.hitCount;
        playerScore.AddScore(type, scoreReward, nameNPC);
    }

    protected virtual void HitCarNPCTrigger(CarNPCInfo carNPCInfo, PlayerScore playerScore)
    {
        if (carNPCInfo.ObjectDataSO == null) return;

        HitObjectType type = carNPCInfo.ObjectDataSO.hitObjectType;
        string nameCar = carNPCInfo.ObjectDataSO.hitObjectName;
        int scoreReward = carNPCInfo.ObjectDataSO.hitCount;
        playerScore.AddScore(type, scoreReward, nameCar);
    }
}
