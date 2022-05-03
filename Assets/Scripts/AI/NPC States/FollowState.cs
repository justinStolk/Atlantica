using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowState : BaseState
{
    [SerializeField] private float minimumDistance, followThreshold;

    [SerializeField] private Transform followTarget;
    
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = minimumDistance;
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
        if (agent.destination != followTarget.position && Vector3.Distance(transform.position, followTarget.position) > followThreshold)
        {
            agent.SetDestination(followTarget.position);
        }
    }
}
