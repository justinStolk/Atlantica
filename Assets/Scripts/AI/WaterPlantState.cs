using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class WaterPlantState : BaseState
{
    private static Plant[] plants;
    private Plant targetPlant;
    private NavMeshAgent agent;
    private GardeningBot bot;

    void Awake()
    {
        plants = FindObjectsOfType<Plant>();
        agent = GetComponent<NavMeshAgent>();
        bot = GetComponent<GardeningBot>();
    }
    public override void OnStateEnter()
    {
        FindPlantToWater();
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        if (bot.WaterLevel == 0)
        {
            owner.SwitchState(typeof(RefillWaterState));
        }
        if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            Debug.LogWarning("Tried to get to a plant, but couldn't find a path to it!");
        }
        if(!agent.hasPath && !agent.pathPending || agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("I've probably reached the plant?");
            if (targetPlant.MoistureLevel < targetPlant.MaxMoistureLevel)
            {
                bot.WaterPlant(targetPlant);
                if (targetPlant.MoistureLevel > targetPlant.MaxMoistureLevel - 1)
                {
                    targetPlant = null;
                    FindPlantToWater();
                }
            }
        }
    }
    private void FindPlantToWater()
    {
        plants.OrderBy(plant => plant.MoistureLevel);
        targetPlant = plants[0];
        Debug.Log(targetPlant.name);
        agent.SetDestination(targetPlant.transform.position);
    }

}
