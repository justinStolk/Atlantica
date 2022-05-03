using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGuard : MonoBehaviour
{
    private FSM stateMachine;
    private void Start()
    {
        stateMachine = new FSM(typeof(PatrolState), GetComponents<BaseState>());
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
