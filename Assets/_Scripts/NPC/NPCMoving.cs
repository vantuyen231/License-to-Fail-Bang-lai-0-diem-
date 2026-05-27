using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMoving : MonoBehaviour
{
    [SerializeField] protected GameObject target;
    [SerializeField] protected NavMeshAgent agent;

    public void Start()
    {
        agent.SetDestination(target.transform.position);
    }
}
