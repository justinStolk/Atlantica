using Cinemachine;
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
    [SerializeField] private float maxSpeed = 5f;

    //[SerializeField] private Camera bpCamera;

    private CinemachineFreeLook camera;

    private PlayerInputActions playerActionsAsset;
    private PlayerManager playerManager;
    private Vector3 forceDirection = Vector3.zero;
    private WaterLevelCheck waterLevelCheck;
    private float upDown;
    private PlayerAnimationManager playerAnim;
    private float boostState;

    private void Start()
    {
        playerActionsAsset = GetComponent<PlayerManager>().playerActionsAsset;

        //playerActionsAsset.Backpack.SwitchToPlayer.started += SwitchToPlayer;
        playerActionsAsset.Backpack.UpDown.started += ctx => upDown = ctx.ReadValue<float>();
        playerActionsAsset.Backpack.UpDown.canceled += ctx => upDown = 0;
        camera = GetComponent<PlayerManager>().camera;
        playerManager = GetComponent<PlayerManager>();
        waterLevelCheck = GetComponent<WaterLevelCheck>();
        playerAnim = GetComponent<PlayerAnimationManager>();
    }

    private void DoBoost()
    {
        boostState = (playerManager.playerActionsAsset.Backpack.Boost.ReadValue<float>());

        if (boostState == 1)
        {
            moveForce = 2f;
        }
        else
        {
            moveForce = 1f;
        }

    }

    public override void OnStateEnter()
    {
        playerActionsAsset.Player.Disable();
        playerActionsAsset.Backpack.Enable();
        //playerAnim.controlBackpack = true;
        playerManager.backpack.transform.Rotate(90f, 0, 0);
        //playerActionsAsset.Backpack.Enable();
        move = playerActionsAsset.Backpack.Move;
        playerManager.backpack.GetComponentInChildren<MeshCollider>().isTrigger = false;

        EventSystem.CallEvent(EventSystem.EventType.ON_BACKPACK_RELEASE);

        Debug.Log("You are now controlling the rogzak!");
    }

    public override void OnStateExit()
    {
        playerActionsAsset.Player.Enable();
        playerActionsAsset.Backpack.Disable();
        playerManager.backpack.GetComponentInChildren<MeshCollider>().isTrigger = true;
    }

    public override void OnStateFixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(camera) * moveForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(camera) * moveForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;
        LookAt();

        forceDirection += Vector3.up * UpDownForce * upDown * Time.fixedDeltaTime;
        DoBoost();
    }

    public override void OnStateUpdate()
    {

    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = -90f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
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
        Vector3 forward = camera.transform.up;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(CinemachineFreeLook camera)
    {
        Vector3 right = camera.transform.right;
        right.y = 0;
        return right.normalized;
    }
    
}
