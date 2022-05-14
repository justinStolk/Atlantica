using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBody : MonoBehaviour
{
    WalkingState playerWalk;
    SwimmingState playerSwim;

    private void Start()
    {
        playerWalk = FindObjectOfType<WalkingState>();
        playerSwim = FindObjectOfType<SwimmingState>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<WalkingState>() == playerWalk)
        {
            if (!playerWalk.InWater)
            {
                playerWalk.InWater = true;
            }

            if (playerWalk.WaterSurface != transform.position.y)
            {
                playerWalk.WaterSurface = transform.position.y;
            }
        }

        if(other.GetComponent<SwimmingState>() == playerSwim)
        {
            if (playerSwim.WaterSurface != transform.position.y)
            {
                playerSwim.WaterSurface = transform.position.y;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SwimmingState>() == playerSwim)
        {
            if (playerSwim.InWater)
            {
                playerSwim.InWater = false;
            }
        }
    }
}
