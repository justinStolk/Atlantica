using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingState : BaseState
{
    public Transform Speaker;
    public bool IsTalking;

    private BaseState previousState;
    public override void OnStateEnter()
    {
        IsTalking = true;
        previousState = owner.PreviousState;
        Vector3 speakerPos = new Vector3(Speaker.position.x, 0, Speaker.position.z);
        Vector3 myPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3.RotateTowards(transform.position, Speaker.position, Mathf.Deg2Rad * 360, 90);
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateFixedUpdate()
    {

    }

    public override void OnStateUpdate()
    {
        if (!IsTalking)
        {
            owner.SwitchState(previousState.GetType());
        }
    }

}
