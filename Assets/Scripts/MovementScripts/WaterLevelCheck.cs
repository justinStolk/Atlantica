using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelCheck : MonoBehaviour
{
    public float WaterSurface, DistanceSurface;
    public bool InWater;
    public float SwimLevel;

    public void GetWaterLevel()
    {
        DistanceSurface = WaterSurface - transform.position.y;
        DistanceSurface = Mathf.Clamp(DistanceSurface, 0, float.MaxValue);
    }
}
