using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{

    private BaseState currentState;
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
        currentState?.OnStateUpdate();
    }

    public void FSMFixedUpdate()
    {
        currentState?.OnStateFixedUpdate();
    }

    public void SwitchState(System.Type newState)
    {
        currentState?.OnStateExit();
        currentState = states[newState];
        currentState?.OnStateEnter();
    }

}
