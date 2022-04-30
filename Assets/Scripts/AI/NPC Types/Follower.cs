using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    private FSM stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new FSM(typeof(FollowState), GetComponents<BaseState>());    
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.FSMUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.FSMFixedUpdate();
    }
}
