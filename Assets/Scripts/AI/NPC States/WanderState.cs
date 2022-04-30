using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WanderState : BaseState
{
    public float MaxWanderDist; 

    private Vector3 targetPosition;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public override void OnStateEnter()
    {
        targetPosition = transform.position + new Vector3(Random.Range(-MaxWanderDist, MaxWanderDist), 0, Random.Range(-MaxWanderDist, MaxWanderDist));
        agent.isStopped = false;
        agent.SetDestination(targetPosition);
    }

    public override void OnStateExit()
    {
        agent.isStopped = true;
    }

    public override void OnStateFixedUpdate()
    {
        
    }

    public override void OnStateUpdate()
    {
        if(!agent.pathPending && !agent.hasPath)
        {
            owner.SwitchState(typeof(IdleState));
        }
    }

}
