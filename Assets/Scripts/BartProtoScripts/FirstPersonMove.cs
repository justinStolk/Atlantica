using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMove : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rigid;
    public Collider collider;

    public Vector3 spawnPoint;

    public float JumpForce = 5;
    public float moveSpeed = 10;
    public float turnSpeed = 4.0f;
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;

    private float rotX;

    private bool isGrounded;
    private bool jumpedTwice;
    private bool isWatered;

    public bool playerInControl;
    public bool canDoubleJump;

    public Transform groundCheck;

    public float groundDistance = 0.4f;

    public LayerMask groundMask;
    public LayerMask waterMask;
    // Start is called before the first frame update
    void Start()
    {
        jumpedTwice = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -50 || Input.GetKeyDown(KeyCode.R))
        {
            transform.position = spawnPoint;
        }
        
        GroundCheck();
        WaterCheck();
        if (playerInControl)
        {
            KeyboardMovement();
            MouseAiming();
            BubbelJump();
        }
    }

    void MouseAiming()
    {
        // get the mouse inputs
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;
        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        // rotate the camera    
        cam.transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + y, 0);
    }

    void KeyboardMovement()
    {
        Vector3 dir = new Vector3(0, 0, 0);
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        transform.Translate(dir * moveSpeed * Time.deltaTime);
        if (Input.GetKeyDown("space") && (isGrounded || isWatered) )
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
        if (isGrounded)
        {
            jumpedTwice = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }

    void BubbelJump()
    {
        if (Input.GetKeyDown("space") && !isGrounded && !jumpedTwice && canDoubleJump)
        {
            rigid.velocity.Set(0, 0, 0);
            Debug.Log("BUBBEL JUMP");
            rigid.AddForce(0, JumpForce * 10, 0);
            jumpedTwice = true;
        }
    }

    void WaterCheck()
    {
        isWatered = Physics.CheckSphere(groundCheck.position, groundDistance, waterMask);
        if (isWatered)
        {
            rigid.drag = 3.5f;
        }
        else
        {
            rigid.drag = 0f;
        }
    }
}
