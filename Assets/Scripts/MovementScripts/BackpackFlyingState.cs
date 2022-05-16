using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackpackFlyingState : BaseState
{
    private PlayerInputActions playerActionsAsset;
    public InputAction move;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveForce = 1f;
    [SerializeField] private Camera bpCamera;

    private PlayerManager playerManager;
    private Vector3 forceDirection = Vector3.zero;
    private WaterLevelCheck waterLevelCheck;

    private void Start()
    {
        playerActionsAsset = GetComponent<PlayerManager>().playerActionsAsset;

        playerActionsAsset.Backpack.SwitchBackPack.started += SwitchBackPack;
        playerManager = GetComponent<PlayerManager>();
        waterLevelCheck = GetComponent<WaterLevelCheck>();
        
    }


    public override void OnStateEnter()
    {
        playerActionsAsset.Player.Disable();
        playerActionsAsset.Backpack.Enable();

        //playerActionsAsset.Backpack.Enable();
        move = playerActionsAsset.Backpack.Move;

        Debug.Log("You are now controlling the rogzak!");
    }

    public override void OnStateExit()
    {
        playerActionsAsset.Player.Enable();
        playerActionsAsset.Backpack.Disable();
    }

    public override void OnStateFixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(bpCamera) * moveForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(bpCamera) * moveForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;
        LookAt();

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

    private void SwitchBackPack(InputAction.CallbackContext obj)
    {
        Debug.Log("test");
        if(waterLevelCheck.InWater == true)
        {
            owner.SwitchState(typeof(SwimmingState));
        }
        else
        {
            owner.SwitchState(typeof(WalkingState));
        }
        playerManager.SwitchPlayer();
    }
}
