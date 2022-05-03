using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resident : MonoBehaviour
{
    private FSM stateMachine;

    private void Start()
    {
        stateMachine = new FSM(typeof(WanderState), GetComponents<BaseState>());
    }
    private void Update()
    {
        stateMachine.FSMUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.FSMFixedUpdate();
    }


}
