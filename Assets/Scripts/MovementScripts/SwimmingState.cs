using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwimmingState : BaseState
{
    public WaterLevelCheck waterLevel;

    public bool SwimmingUp = false;
    public bool SwimmingDown = false;
    
    public LayerMask GroundMask;

    //Input fields
    private ThirdPersonActions playerActionsAsset;
    private InputAction move;

    //Movement fields
    private Rigidbody rb;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField] private float moveForce = 1f;
    [SerializeField] private float UpwardForce = 5f;
    [SerializeField] private float maxSpeed = 5f;

    [SerializeField] private Camera playerCamera;

    public override void OnStateEnter()
    {
        Debug.Log("SWIM");
        waterLevel.InWater = true;

        rb = GetComponent<Rigidbody>();
        playerActionsAsset = new ThirdPersonActions();

        playerActionsAsset.Player.SwimUp.started += SwimUp;
        playerActionsAsset.Player.SwimDown.started += SwimDown;
        playerActionsAsset.Player.SwimUp.canceled += ResetVelocity;
        playerActionsAsset.Player.SwimDown.canceled += ResetVelocity;
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Enable();
        rb.drag = 8f;
        rb.useGravity = false;
    }


    public override void OnStateExit()
    {
        playerActionsAsset.Player.SwimUp.started -= SwimUp;
        playerActionsAsset.Player.SwimDown.started -= SwimDown;

        playerActionsAsset.Player.Disable();
        rb.useGravity = true;
    }

    public override void OnStateFixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * moveForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * moveForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }

        LookAt();

        if (waterLevel.InWater)
        {
            waterLevel.GetWaterLevel();
        }

        if(SwimmingUp == true)
        {
            forceDirection += Vector3.up * UpwardForce;
        }

        if(SwimmingDown == true)
        {
            forceDirection += Vector3.down * UpwardForce;
        }
        

        if (Physics.Raycast(transform.position, Vector3.down, 1.1f, GroundMask)){

            if (waterLevel.Distance_Surface < waterLevel.swimLevel)
            {
                owner.SwitchState(typeof(WalkingState));
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, float.MinValue, waterLevel.WaterSurface - waterLevel.swimLevel), transform.position.z);
        }
    }

    public override void OnStateUpdate()
    {

    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void SwimUp(InputAction.CallbackContext obj)
    {
        Debug.Log("UP");
        SwimmingUp = true;
    }

    private void SwimDown(InputAction.CallbackContext obj)
    {
        Debug.Log("Down");
        SwimmingDown = true;
    }

    private void ResetVelocity(InputAction.CallbackContext obj)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        SwimmingDown = false;
        SwimmingUp = false;
    }
}
