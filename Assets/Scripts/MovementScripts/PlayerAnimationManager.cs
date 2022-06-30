using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public Rigidbody Rb;
    public bool Jumping = false;
    public GameObject BackPackHolder;
    public GameObject Player;

    private Animator anim;
    private float walkingSpeed;
    private WaterLevelCheck waterLevel;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        waterLevel = GetComponent<WaterLevelCheck>();
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
            walkingSpeed = Rb.velocity.magnitude;
            anim.SetFloat("WalkingSpeed", walkingSpeed);

            if(Jumping == false)
            {
                ResetJumpAnim();
                anim.SetBool("Jumping", false);
            }

            if (Jumping == true)
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
