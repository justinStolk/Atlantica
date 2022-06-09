using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator anim;
    public Rigidbody rb;
    public bool jumping = false;

    private float WalkingSpeed;
    private WaterLevelCheck waterLevel;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        waterLevel = GetComponent<WaterLevelCheck>();
        //rb.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(waterLevel.InWater == false)
        {
            anim.SetBool("Swimming", false);
            WalkingSpeed = rb.velocity.magnitude;
            anim.SetFloat("WalkingSpeed", WalkingSpeed);
            if(jumping == true)
            {
                anim.SetBool("Jumping", true);
                jumping = false;
            }
            else
            {
                anim.SetFloat("WalkingSpeed", WalkingSpeed);
            }
        }
        if(waterLevel.InWater == true)
        {
            anim.SetBool("Swimming", true);
        }
    }
}
