using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStation : MonoBehaviour
{
    public Transform StationWaitPoint;

    [SerializeField] private float waterFlowSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool FillingGardeningBot(GardeningBot botToFill)
    {
        return botToFill.FillingBot(waterFlowSpeed);
    }
}
