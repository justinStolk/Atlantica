using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WanderState : BaseState
{
    public float MaxWanderDist;
    public float IdleTime;

    private float idleTimer;
    private Vector3 targetPosition;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public override void OnStateEnter()
    {
        SetNewWanderDestination();
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
        if(agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            SetNewWanderDestination();
        }
        if(!agent.pathPending && !agent.hasPath)
        {
            idleTimer += Time.deltaTime;
            if(idleTimer >= IdleTime)
            {
                idleTimer = 0;
                SetNewWanderDestination();
            }
            //owner.SwitchState(typeof(IdleState));
        }
    }

    private void SetNewWanderDestination()
    {
        targetPosition = transform.position + new Vector3(Random.Range(-MaxWanderDist, MaxWanderDist), 0, Random.Range(-MaxWanderDist, MaxWanderDist));
        agent.isStopped = false;
        agent.SetDestination(targetPosition);
    }

}
