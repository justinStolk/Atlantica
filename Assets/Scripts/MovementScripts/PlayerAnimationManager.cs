using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator anim;
    public Rigidbody rb;
    public bool jumping = false;
    public GameObject backPackHolder;
    public GameObject player;

    private bool setPosition;
    private float WalkingSpeed;
    private WaterLevelCheck waterLevel;
    private PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        waterLevel = GetComponent<WaterLevelCheck>();
        playerManager = GetComponent<PlayerManager>();
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
            setPosition = false;
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
            backPackHolder.transform.eulerAngles = new Vector3(transform.rotation.x + 90, player.transform.eulerAngles.y, transform.rotation.z);

            if(setPosition == false)
            {
                backPackHolder.transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z + 0.3f);
                setPosition = true;
            }
        }
    }
}
