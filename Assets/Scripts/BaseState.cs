using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected FSM owner;

    public void InitializeState(FSM owner) 
    {
        this.owner = owner;
    }
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateFixedUpdate();
    public abstract void OnStateExit();

}
