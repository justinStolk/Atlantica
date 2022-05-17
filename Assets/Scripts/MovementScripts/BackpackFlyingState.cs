using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackpackFlyingState : BaseState
{
    public InputAction move;
    public float UpDownForce;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveForce = 1f;
    [SerializeField] private Camera bpCamera;

    private PlayerInputActions playerActionsAsset;
    private PlayerManager playerManager;
    private Vector3 forceDirection = Vector3.zero;
    private WaterLevelCheck waterLevelCheck;
    private float upDown;

    private void Start()
    {
        playerActionsAsset = GetComponent<PlayerManager>().playerActionsAsset;

        //playerActionsAsset.Backpack.SwitchToPlayer.started += SwitchToPlayer;
        playerActionsAsset.Backpack.UpDown.started += ctx => upDown = ctx.ReadValue<float>();
        playerActionsAsset.Backpack.UpDown.canceled += ctx => upDown = 0;
        playerManager = GetComponent<PlayerManager>();
        waterLevelCheck = GetComponent<WaterLevelCheck>();
    }

   

    public override void OnStateEnter()
    {
        playerActionsAsset.Player.Disable();
        playerActionsAsset.Backpack.Enable();

        //playerActionsAsset.Backpack.Enable();
        move = playerActionsAsset.Backpack.Move;

        EventSystem.CallEvent(EventSystem.EventType.ON_BACKPACK_RELEASE);

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

        forceDirection += Vector3.up * UpDownForce * upDown * Time.fixedDeltaTime;
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

    //private void SwitchBackPack(InputAction.CallbackContext obj)
    //{
    //    Debug.Log("SwitchToPlayer");
    //    if(waterLevelCheck.InWater == true)
    //    {
    //        owner.SwitchState(typeof(SwimmingState));
    //    }
    //    else
    //    {
    //        owner.SwitchState(typeof(WalkingState));
    //    }
    //    playerManager.SwitchPlayer();
    //}
}
