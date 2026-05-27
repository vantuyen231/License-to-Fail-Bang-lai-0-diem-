using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRagdoll : TuyenMonoBehaviour
{
    [SerializeField] protected Animator npcAnimator;
    [SerializeField] protected CapsuleCollider npcCapsuleCollider;
    [SerializeField] public List<Rigidbody> npcRigidbodies = new List<Rigidbody>();
    [SerializeField] public List<Collider> npcColliders = new List<Collider>();
    [SerializeField] protected bool isRagdoll = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNPCRigidbodies();
        this.LoadNPCAnimator();
        this.LoadNPCCapsuleCollider();
        this.LoadNPCCollider();
    }

    protected virtual void LoadNPCAnimator()
    {
        if(npcAnimator != null) return;
        TryGetComponent(out npcAnimator);
    }

    protected virtual void LoadNPCCapsuleCollider()
    {
        if(npcCapsuleCollider != null) return;
        TryGetComponent(out npcCapsuleCollider);
    }

    protected virtual void LoadNPCRigidbodies()
    {
        if (npcRigidbodies.Count > 0) return;
        this.npcRigidbodies.AddRange(GetComponentsInChildren<Rigidbody>());
    }

    protected virtual void LoadNPCCollider()
    {
        if (npcColliders.Count > 0) return;
        this.npcColliders.AddRange(GetComponentsInChildren<Collider>());
    }


    protected override void Awake()
    {
        //TryGetComponent(out npcAnimator);
        //TryGetComponent(out npcCapsuleCollider);
        //if (npcAnimator == null) return;

        //GetComponentsInChildren(npcColliders);
        //GetComponentsInChildren(npcRigidbodies);

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
