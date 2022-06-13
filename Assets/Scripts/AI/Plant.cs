using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public float MoistureLevel { get; set; }
    public float MaxMoistureLevel { get { return maxMoistureLevel; } }
    
    [SerializeField] private float maxMoistureLevel;
    [SerializeField] private float moistureDecreaseValue;
    [SerializeField] private float decaySpeed;

    private float decayLevel = 0;


    // Start is called before the first frame update
    void Awake()
    {
        MoistureLevel = Random.Range(4, maxMoistureLevel);
    }

    // Update is called once per frame
    void Update()
    {

        if(MoistureLevel < 0)
        {
            MoistureLevel = 0;
        }
        if(MoistureLevel == 0)
        {
            decayLevel += decaySpeed * Time.deltaTime;
            //Debug.Log("Decaying level at: " + decayLevel);
            if (decayLevel >= 10)
            {
                Debug.Log("A plant has died...");
                Destroy(this.gameObject);
            }
            return;
        }
        MoistureLevel -= moistureDecreaseValue * Time.deltaTime;
    }

    public void WaterPlant(float wateringAmount)
    {
        MoistureLevel += wateringAmount;
        //Debug.Log("Plant's  moisture level: " + MoistureLevel);
        if(MoistureLevel > maxMoistureLevel)
        {
            MoistureLevel = maxMoistureLevel;
        }
    }

}
