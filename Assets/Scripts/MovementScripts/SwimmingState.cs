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
    
    public LayerMask GroundMask;

    //Input fields

    //Movement fields
    [SerializeField] private float moveForce = 1f;
    [SerializeField] private float UpwardForce = 5f;
    [SerializeField] private float maxSpeed = 5f;


    public WaterLevelCheck waterLevel;
    private PlayerManager playerManager;
    private Rigidbody rb;
    private Vector3 forceDirection = Vector3.zero;
    private CinemachineFreeLook camera;
    private float sprintState;
    private BackpackFollow backpackFollow;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        camera = GetComponent<PlayerManager>().camera;
        rb = GetComponent<Rigidbody>();
        waterLevel = GetComponent<WaterLevelCheck>();
        backpackFollow = playerManager.backpack.GetComponent<BackpackFollow>();
    }

    public override void OnStateEnter()
    {
        Debug.Log("SWIM");
        waterLevel.InWater = true;

        playerManager.playerActionsAsset.Player.SwimUp.started += SwimUp;
        playerManager.playerActionsAsset.Player.SwimDown.started += SwimDown;
        playerManager.playerActionsAsset.Player.SwimUp.canceled += ResetVelocity;
        playerManager.playerActionsAsset.Player.SwimDown.canceled += ResetVelocity;
        
        rb.drag = 8f;
        rb.useGravity = false;
    }

    public override void OnStateExit()
    {
        playerManager.playerActionsAsset.Player.SwimUp.started -= SwimUp;
        playerManager.playerActionsAsset.Player.SwimDown.started -= SwimDown;

        rb.useGravity = true;
        Debug.Log("Changing");
    }

    public override void OnStateFixedUpdate()
    {
        forceDirection += playerManager.move.ReadValue<Vector2>().x * GetCameraRight(camera) * moveForce;
        forceDirection += playerManager.move.ReadValue<Vector2>().y * GetCameraForward(camera) * moveForce;

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

        DoSprint();

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

        if (playerManager.move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }

    private Vector3 GetCameraForward(CinemachineFreeLook camera)
    {
        Vector3 forward = camera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(CinemachineFreeLook camera)
    {
        Vector3 right = camera.transform.right;
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

    private void DoSprint()
    {
        sprintState = (playerManager.playerActionsAsset.Player.Sprint.ReadValue<float>());

        if (sprintState == 1 && backpackFollow.followPlayer == true)
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
