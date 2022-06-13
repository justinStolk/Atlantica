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

    void PlaySound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    // Update is called once per frame
    void Update()
    {
        if(waterLevel.InWater == false)
        {
            anim.SetBool("Swimming", false);
            WalkingSpeed = rb.velocity.magnitude;
            anim.SetFloat("WalkingSpeed", WalkingSpeed);

            if(jumping == false)
            {
                ResetJumpAnim();
                anim.SetBool("Jumping", false);
            }

            if (jumping == true)
            {
                anim.SetBool("Jumping", true);
            }
            
        }
        if(waterLevel.InWater == true)
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Swimming", true);
        }
    }

    IEnumerator ResetJumpAnim()
    {
        yield return new WaitForSeconds(1f);
    }
}
