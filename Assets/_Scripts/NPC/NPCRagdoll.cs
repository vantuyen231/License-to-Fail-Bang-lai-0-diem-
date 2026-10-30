using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCRagdoll : TuyenMonoBehaviour
{
    [SerializeField] protected NPCCtrl npcCtrl;
    [SerializeField] protected Animator npcAnimator;
    [SerializeField] protected CapsuleCollider npcCapsuleCollider;
    [SerializeField] public List<Rigidbody> npcRigidbodies = new List<Rigidbody>();
    [SerializeField] public List<Collider> npcColliders = new List<Collider>();
    [SerializeField] protected bool isRagdoll = false;
    public bool IsRagdoll => isRagdoll;
    [SerializeField] protected NavMeshAgent navMeshAgent;

    #region Load Component
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNPCCtrl();
        this.LoadNPCRigidbodies();
        this.LoadNPCAnimator();
        this.LoadNPCCapsuleCollider();
        this.LoadNPCCollider();
        this.LoadNPCNav();
    }

    protected virtual void LoadNPCCtrl()
    {
        if (npcCtrl != null) return;
        npcCtrl = GetComponent<NPCCtrl>();
        Debug.Log(transform.name + ": LoadNPCCtrl", gameObject);
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
    #endregion

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
        //isRagdoll = !isRagdoll;
        isRagdoll = true;
        if (npcCtrl != null && npcCtrl.NpcAudio != null)
        {
            npcCtrl.NpcAudio.HitNPC();
            npcCtrl.NpcAudio.HurtSound();
        }
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
