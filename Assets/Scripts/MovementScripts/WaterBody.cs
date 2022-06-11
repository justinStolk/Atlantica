using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBody : MonoBehaviour
{
    WalkingState playerWalk;
    SwimmingState playerSwim;

    private FMODUnity.StudioEventEmitter eventEmitterRef;

    private void Start()
    {
        playerWalk = FindObjectOfType<WalkingState>();
        playerSwim = FindObjectOfType<SwimmingState>();
        eventEmitterRef = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<WalkingState>() == playerWalk)
        {
            if (!playerWalk.waterLevel.InWater)
            {
                playerWalk.waterLevel.InWater = true;
            }

            if (playerWalk.waterLevel.WaterSurface != transform.position.y)
            {
                playerWalk.waterLevel.WaterSurface = transform.position.y;
            }

        }

        if(other.GetComponent<SwimmingState>() == playerSwim)
        {
            if (playerSwim.waterLevel.WaterSurface != transform.position.y)
            {
                playerSwim.waterLevel.WaterSurface = transform.position.y;
            }

            eventEmitterRef.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SwimmingState>() == playerSwim)
        {
            if (playerSwim.waterLevel.InWater)
            {
                playerSwim.waterLevel.InWater = false;
            }
            eventEmitterRef.Stop();
        }
    }
}
