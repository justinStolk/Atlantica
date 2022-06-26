using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwimmingState : BaseState
{

    public bool SwimmingUp = false;
    public bool SwimmingDown = false;
    public WaterLevelCheck WaterLevel;
    public LayerMask GroundMask;


    //Movement fields
    [SerializeField] private float moveForce = 1f;
    [SerializeField] private float UpwardForce = 5f;
    [SerializeField] private float maxSpeed = 5f;


    private PlayerManager playerManager;
    private Rigidbody rb;
    private Vector3 forceDirection = Vector3.zero;
    private CinemachineFreeLook camera;
    private float sprintState;
    private BackpackFollow backpackFollow;
    private ThirdPersonCamera cameraControl;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        camera = GetComponent<PlayerManager>().Camera;
        rb = GetComponent<Rigidbody>();
        WaterLevel = GetComponent<WaterLevelCheck>();
        backpackFollow = playerManager.Backpack.GetComponent<BackpackFollow>();
        cameraControl = GetComponent<ThirdPersonCamera>();

    }

    public override void OnStateEnter()
    {
        Debug.Log("SWIM");
        WaterLevel.InWater = true;

        playerManager.PlayerActionsAsset.Player.SwimUp.started += SwimUp;
        playerManager.PlayerActionsAsset.Player.SwimDown.started += SwimDown;
        playerManager.PlayerActionsAsset.Player.SwimUp.canceled += ResetVelocity;
        playerManager.PlayerActionsAsset.Player.SwimDown.canceled += ResetVelocity;
        
        rb.drag = 8f;
        rb.useGravity = false;
    }

    public override void OnStateExit()
    {
        playerManager.PlayerActionsAsset.Player.SwimUp.started -= SwimUp;
        playerManager.PlayerActionsAsset.Player.SwimDown.started -= SwimDown;

        rb.useGravity = true;
        Debug.Log("Changing");
    }

    public override void OnStateFixedUpdate()
    {
        forceDirection += playerManager.Move.ReadValue<Vector2>().x * cameraControl.GetCameraRight(camera) * moveForce;
        forceDirection += playerManager.Move.ReadValue<Vector2>().y * cameraControl.GetCameraForward(camera) * moveForce;

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

        cameraControl.PlayerLookAt();

        if (WaterLevel.InWater)
        {
            WaterLevel.GetWaterLevel();
        }

        if(SwimmingUp == true)
        {
            forceDirection += Vector3.up * UpwardForce;
        }

        if(SwimmingDown == true)
        {
            forceDirection += Vector3.down * UpwardForce;
        }

        DoSprint();

        if (Physics.Raycast(transform.position, Vector3.down, 1.1f, GroundMask)){

            if (WaterLevel.Distance_Surface < WaterLevel.SwimLevel)
            {
                owner.SwitchState(typeof(WalkingState));
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, float.MinValue, WaterLevel.WaterSurface - WaterLevel.SwimLevel), transform.position.z);
        }

    }

    public override void OnStateUpdate()
    {

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

    private void DoSprint()
    {
        sprintState = (playerManager.PlayerActionsAsset.Player.Sprint.ReadValue<float>());

        if (sprintState == 1 && backpackFollow.FollowPlayer == true)
        {
            maxSpeed = 20f;
            moveForce = 4f;
        }
        else
        {
            maxSpeed = 5f;
            moveForce = 1f;
        }

    }
}
