using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRagdoll : MonoBehaviour
{
    [SerializeField] protected Animator npcAnimator;
    [SerializeField] protected CapsuleCollider npcCapsuleCollider;
    [SerializeField] public List<Rigidbody> npcRigidbodies = new List<Rigidbody>();
    [SerializeField] public List<Collider> npcColliders = new List<Collider>();
    [SerializeField] protected bool isRagdoll = false;

    private void Awake()
    {
        TryGetComponent(out npcAnimator);
        TryGetComponent(out npcCapsuleCollider);
        if (npcAnimator == null) return;

        GetComponentsInChildren(npcColliders);
        GetComponentsInChildren(npcRigidbodies);

        for (int i = 0; i < npcRigidbodies.Count; i++)
        {
            npcRigidbodies[i].isKinematic = true;
            npcColliders[i].isTrigger = true;
        }

        npcCapsuleCollider.isTrigger = false;
    }

    public void EnableRagdoll()
    {
        isRagdoll = !isRagdoll;
        for (int i = 0; i < npcRigidbodies.Count; i++)
        {
            npcRigidbodies[i].isKinematic = false;
            npcRigidbodies[i].velocity = Vector3.zero;
            npcColliders[i].isTrigger = false;
            npcRigidbodies[i].WakeUp();
        }
        npcAnimator.enabled = false;
        npcCapsuleCollider.isTrigger = true;
    }



}
