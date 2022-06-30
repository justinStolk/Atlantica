using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackpackFlyingState : BaseState
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveForce = 1f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField]private float UpDownForce = 15f;
    

    private float upDown;
    private float boostState;
    private CinemachineFreeLook camera;
    private Vector3 forceDirection = Vector3.zero;
    private PlayerInputActions playerActionsAsset;

    //Scripts
    private PlayerManager playerManager;
    private void Start()
    {
        playerActionsAsset = GetComponent<PlayerManager>().PlayerActionsAsset;

        playerActionsAsset.Backpack.UpDown.started += ctx => upDown = ctx.ReadValue<float>();
        playerActionsAsset.Backpack.UpDown.canceled += ctx => upDown = 0;
        camera = GetComponent<PlayerManager>().Camera;
        playerManager = GetComponent<PlayerManager>();
    }


    public override void OnStateEnter()
    {
        playerActionsAsset.Player.Disable();
        playerActionsAsset.Backpack.Enable();

        playerManager.Backpack.transform.Rotate(90f, 0, 0);
        playerManager.Backpack.GetComponentInChildren<MeshCollider>().isTrigger = false;

        EventSystem.CallEvent(EventSystem.EventType.ON_BACKPACK_RELEASE);

        Debug.Log("You are now controlling the rogzak!");
    }

    public override void OnStateExit()
    {
        playerActionsAsset.Player.Enable();
        playerActionsAsset.Backpack.Disable();
        playerManager.Backpack.GetComponentInChildren<MeshCollider>().isTrigger = true;
    }

    public override void OnStateFixedUpdate()
    {
        forceDirection += playerManager.PlayerActionsAsset.Backpack.Move.ReadValue<Vector2>().x * GetCameraRight(camera) * moveForce;
        forceDirection += playerManager.PlayerActionsAsset.Backpack.Move.ReadValue<Vector2>().y * GetCameraForward(camera) * moveForce;

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

        if (playerManager.PlayerActionsAsset.Backpack.Move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
    private void DoBoost()
    {
        boostState = (playerManager.PlayerActionsAsset.Backpack.Boost.ReadValue<float>());

        if (boostState == 1)
        {
            moveForce = 2f;
        }
        else
        {
            moveForce = 1f;
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
