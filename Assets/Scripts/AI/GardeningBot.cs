using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardeningBot : MonoBehaviour
{
    public float WaterLevel { get; private set; }

    [SerializeField] private float wateringSpeed;
    [SerializeField] private float maxWaterLevel;
 
    private FSM stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        WaterLevel = maxWaterLevel;
        stateMachine = new FSM(typeof(WaterPlantState), GetComponents<BaseState>());
 
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.FSMUpdate();
    }

    public bool FillingBot(float fillAmount)
    {
        WaterLevel += fillAmount * Time.deltaTime;
        Debug.Log("Water level: " + WaterLevel);
        if(WaterLevel > maxWaterLevel)
        {
            WaterLevel = maxWaterLevel;
            return true;
        }
        return false;
    }
    public void WaterPlant(Plant plantToWater)
    {
        float wateringAmount = wateringSpeed * Time.deltaTime;
        if(WaterLevel >= wateringAmount && plantToWater.MoistureLevel < plantToWater.MaxMoistureLevel)
        {
            plantToWater.WaterPlant(wateringAmount);
            WaterLevel -= wateringAmount;
        }
    }


}
