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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SwimmingState>() == playerSwim)
        {
            
            eventEmitterRef.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<WalkingState>() == playerWalk)
        {
            if (!playerWalk.PlayerManager.waterLevelCheck.InWater)
            {
                playerWalk.PlayerManager.waterLevelCheck.InWater = true;
            }

            if (playerWalk.PlayerManager.waterLevelCheck.WaterSurface != transform.position.y)
            {
                playerWalk.PlayerManager.waterLevelCheck.WaterSurface = transform.position.y;
            }

        }

        if(other.GetComponent<SwimmingState>() == playerSwim)
        {
            if (playerSwim.playerManager.waterLevelCheck.WaterSurface != transform.position.y)
            {
                playerSwim.playerManager.waterLevelCheck.WaterSurface = transform.position.y;
            }
            //musicSystem.StartMusic();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SwimmingState>() == playerSwim)
        {
            if (playerSwim.playerManager.waterLevelCheck.InWater)
            {
                playerSwim.playerManager.waterLevelCheck.InWater = false;
                //musicSystem.StopMusic();
                eventEmitterRef.SendMessage("Stop");
                Debug.Log("UIT T WATER");
            }
        }
    }

    
}
