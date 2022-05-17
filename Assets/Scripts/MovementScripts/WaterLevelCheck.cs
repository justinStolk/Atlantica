using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelCheck : MonoBehaviour
{
    public float WaterSurface, Distance_Surface;
    public bool InWater;
    public float swimLevel = 1.25f;

    public void GetWaterLevel()
    {
        Distance_Surface = WaterSurface - transform.position.y;
        Distance_Surface = Mathf.Clamp(Distance_Surface, 0, float.MaxValue);
    }
}
