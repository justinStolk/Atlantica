using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PatrolState : BaseState
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float patrolWaitTime;

    private float patrolTimer;
    private int patrolIndex = 0;
    private NavMeshAgent agent;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public override void OnStateEnter()
    {
        agent.SetDestination(patrolPoints[patrolIndex].position);
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateFixedUpdate()
    {

    }

    public override void OnStateUpdate()
    {
        if (!agent.pathPending && !agent.hasPath)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaitTime)
            {
                patrolTimer = 0;
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[patrolIndex].position);
            }
        }
    }

}
