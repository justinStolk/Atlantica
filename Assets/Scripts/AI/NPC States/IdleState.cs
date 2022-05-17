using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    [SerializeField] private float idleTime;
    private BaseState prevState;
    private float idleTimer;
    public override void OnStateEnter()
    {
        prevState = owner.PreviousState;
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
            owner.SwitchState(prevState.GetType());
        }
    }

}
