using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMove : MonoBehaviour
{
    public Transform cam;
    public Rigidbody rigid;
    public Collider collider;
    public Vector3 spawnPoint;

    public float JumpForce = 5;
    public float moveSpeed = 10;
    public float turnSpeed = 4.0f;
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    public float turnSmoothTime = 0.1f;

    private float rotX;
    private float turnSmoothVelocity;

    private bool isGrounded;
    private bool isOnTurning;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.y < -50 || Input.GetKeyDown(KeyCode.R))
        {
            transform.position = spawnPoint;
        }

        GroundCheck();
        //Debug.Log(isGrounded);
        KeyboardMovement();
        //MouseAiming();


        if (isOnTurning)
        {
            JumpForce = 95;
        }
        else
        {
            JumpForce = 65;
        }
    }

    void MouseAiming()
    {
        //FIRSTPERSON
        //// get the mouse inputs
        //float y = Input.GetAxis("Mouse X") * turnSpeed;
        //rotX += Input.GetAxis("Mouse Y") * turnSpeed;
        //// clamp the vertical rotation
        //rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        //// rotate the camera    
        //cam.transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + y, 0);
    }

    void KeyboardMovement()
    {
        Vector3 dir = new Vector3(0, 0, 0);
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown("space") && isGrounded)
        {
            //Debug.Log("JUMP");
            //transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0,100000,0), JumpForce);
            rigid.AddForce(0, JumpForce * 10, 0);
            //Debug.Log("jumped");
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }
}
