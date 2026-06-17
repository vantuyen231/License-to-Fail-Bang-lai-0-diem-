using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCRagdoll : TuyenMonoBehaviour
{
    [SerializeField] protected Animator npcAnimator;
    [SerializeField] protected CapsuleCollider npcCapsuleCollider;
    [SerializeField] public List<Rigidbody> npcRigidbodies = new List<Rigidbody>();
    [SerializeField] public List<Collider> npcColliders = new List<Collider>();
    [SerializeField] protected bool isRagdoll = false;
    public bool IsRagdoll => isRagdoll;
    [SerializeField] protected NavMeshAgent navMeshAgent;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNPCRigidbodies();
        this.LoadNPCAnimator();
        this.LoadNPCCapsuleCollider();
        this.LoadNPCCollider();
        this.LoadNPCNav();
    }

    protected virtual void LoadNPCAnimator()
    {
        if(npcAnimator != null) return;
        TryGetComponent(out npcAnimator);
    }
    protected virtual void LoadNPCNav()
    {
        if (navMeshAgent != null) return;
        TryGetComponent(out navMeshAgent);
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
        navMeshAgent.enabled = false;
        npcCapsuleCollider.isTrigger = true;
    }

    public void DisableRagdoll()
    {
        isRagdoll = false;
        for (int i = 0; i < npcRigidbodies.Count; i++)
        {
            npcRigidbodies[i].isKinematic = true;
            npcColliders[i].isTrigger = true;
        }


        npcAnimator.enabled = true;
        navMeshAgent.enabled = true;
        npcCapsuleCollider.isTrigger = false;
    }

}
