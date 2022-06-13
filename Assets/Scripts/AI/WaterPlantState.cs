using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class WaterPlantState : BaseState
{
    [SerializeField] private float preferredMinimalWaterLevel;
    [SerializeField] private float wateringSpeed;

    private static List<Plant> plants;
    private Plant targetPlant;
    private NavMeshAgent agent;
    private GardeningBot bot;

    void Awake()
    {
        plants = FindObjectsOfType<Plant>().ToList();
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
        if (bot.WaterLevel <= 0)
        {
            bot.WaterLevel = 0;
            owner.SwitchState(typeof(RefillWaterState));
            return;
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
                WaterPlant(targetPlant);
            }
            if (targetPlant.MoistureLevel >= targetPlant.MaxMoistureLevel - 1)
            {
                Debug.Log("This plant has had enough!");
                targetPlant = null;
                FindPlantToWater();
                return;
            }
        }else if(targetPlant == null)
        {
            FindPlantToWater();
        }
    }

    private void FindPlantToWater()
    {
        if (bot.WaterLevel <= preferredMinimalWaterLevel)
        {
            owner.SwitchState(typeof(RefillWaterState));
            return;
        }
        targetPlant = plants.OrderBy(plant => plant.MoistureLevel).First();
        if(targetPlant == null)
        {
            plants.Remove(targetPlant);
            FindPlantToWater();
            return;
        }
        Debug.Log(targetPlant.name);
        agent.SetDestination(targetPlant.transform.position);
    }
    private void WaterPlant(Plant plantToWater)
    {
        float wateringAmount = wateringSpeed * Time.deltaTime;
        if (bot.WaterLevel >= wateringAmount)
        {
            plantToWater.WaterPlant(wateringAmount);
            bot.WaterLevel -= wateringAmount;
        }
    }

}
