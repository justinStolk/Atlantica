using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowState : BaseState
{
    [SerializeField] private float followingDistance;

    [SerializeField] private Transform followTarget;
    
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = followingDistance;
    }
    public override void OnStateEnter()
    {
        agent.SetDestination(followTarget.position);
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateFixedUpdate()
    {
        
    }

    public override void OnStateUpdate()
    {
        if (!agent.pathPending && !agent.hasPath || agent.destination != followTarget.position)
        {
            agent.SetDestination(followTarget.position);
        }
    }
}
