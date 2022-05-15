using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackFlyingState : BaseState
{
    public PlayerInputActions playerActionsAsset;


    public override void OnStateEnter()
    {
        playerActionsAsset.Backpack.Enable();
        Debug.Log("You are now controlling the rogzak!");
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateFixedUpdate()
    {
        
    }

    public override void OnStateUpdate()
    {

    }

    
}
