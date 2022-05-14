using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public BaseState CurrentState { get; private set; }
    public BaseState PreviousState { get; private set; }
    
    private Dictionary<System.Type, BaseState> states = new();


    public FSM(System.Type startState, params BaseState[] allStates)
    {
        foreach (BaseState state in allStates)
        {
            state.InitializeState(this);
            states.Add(state.GetType(), state);
        }
        SwitchState(startState);
    }
    public void FSMUpdate()
    {
        CurrentState?.OnStateUpdate();
    }

    public void FSMFixedUpdate()
    {
        CurrentState?.OnStateFixedUpdate();
    }

    public void SwitchState(System.Type newState)
    {
        PreviousState = CurrentState;
        CurrentState?.OnStateExit();
        CurrentState = states[newState];
        CurrentState?.OnStateEnter();
    }

}
