using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardeningBot : MonoBehaviour
{
    public float WaterLevel;
    public float MaxWaterLevel;
 
    private FSM stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        WaterLevel = MaxWaterLevel;
        stateMachine = new FSM(typeof(WaterPlantState), GetComponents<BaseState>());
 
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.FSMUpdate();
    }
}
