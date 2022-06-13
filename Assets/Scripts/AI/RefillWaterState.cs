using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class RefillWaterState : BaseState
{
    private static WaterStation[] waterStations;
    private WaterStation targetWaterStation;
    private NavMeshAgent agent;
    private GardeningBot bot;

    private void Awake()
    {
        waterStations = FindObjectsOfType<WaterStation>();
        agent = GetComponent<NavMeshAgent>();
        bot = GetComponent<GardeningBot>();
    }
    public override void OnStateEnter()
    {
        waterStations.OrderBy(station => Vector3.Distance(this.transform.position, station.transform.position)).First();
        targetWaterStation = waterStations[0];
        agent.SetDestination(targetWaterStation.StationWaitPoint.position);
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            Debug.LogWarning("Tried to get to the closest water station, but couldn't find a path to it!");
        }
        if (!agent.hasPath && !agent.pathPending)
        {
            Debug.Log("I've probably reached the water station?");
            FillBot(targetWaterStation.waterFlowSpeed);
            if (bot.WaterLevel >= bot.MaxWaterLevel)
            {
                bot.WaterLevel = bot.MaxWaterLevel;
                owner.SwitchState(typeof(WaterPlantState));
                return;
            }
        }
    }
    private void FillBot(float fillAmount)
    {
        bot.WaterLevel += fillAmount * Time.deltaTime;
    }


}
