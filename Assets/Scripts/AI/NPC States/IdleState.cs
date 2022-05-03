using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    [SerializeField] private float idleTime;
    private float idleTimer;
    public override void OnStateEnter()
    {
        idleTimer = 0;
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateFixedUpdate()
    {
        
    }

    public override void OnStateUpdate()
    {
        idleTimer += Time.deltaTime;
        if(idleTimer >= idleTime)
        {
            owner.SwitchState(typeof(WanderState));
        }
    }

}
