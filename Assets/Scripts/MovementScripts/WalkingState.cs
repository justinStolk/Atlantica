using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class WalkingState : BaseState
{
    public WaterLevelCheck WaterLevel;
    public LayerMask GroundMask;
    public LayerMask WaterMask;
    public GameObject Feet;
    public PlayerManager PlayerManager;
    public Rigidbody Rb;


    //Movement fields
    [SerializeField] private float moveForce = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float jumpButtonTime = 2f;

    private CinemachineFreeLook camera;
    private Vector3 forceDirection = Vector3.zero;
    private PlayerAnimationManager playerAnim;
    private float jumpState;
    private ThirdPersonCamera cameraControl;

    private void Awake()
    {
        
    }

    private void Start()
    {
        camera = GetComponent<PlayerManager>().Camera;
        cameraControl = GetComponent<ThirdPersonCamera>();
        playerAnim = GetComponent<PlayerAnimationManager>();
    }

    public override void OnStateEnter()
    {
        PlayerManager.PlayerActionsAsset.Player.Jump.started += DoJump;
        Debug.Log("WALK");
        WaterLevel.InWater = false;
        Rb.drag = 3.5f;
    }

    private void DoSprint()
    {
        jumpState = (PlayerManager.PlayerActionsAsset.Player.Sprint.ReadValue<float>());

        if (jumpState == 1 && IsGrounded())
        {
            maxSpeed = 10f;
        }
        else
        {
            maxSpeed = 5f;

        }

    }

    public override void OnStateExit()
    {
        PlayerManager.PlayerActionsAsset.Player.Jump.started -= DoJump;
    }

    

    public override void OnStateFixedUpdate()
    {

        forceDirection += PlayerManager.Move.ReadValue<Vector2>().x * cameraControl.GetCameraRight(camera) * moveForce;
        forceDirection += PlayerManager.Move.ReadValue<Vector2>().y * cameraControl.GetCameraForward(camera) * moveForce;

        Rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (Rb.velocity.y < 0f)
        {
            Rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        Vector3 horizontalVelocity = Rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            Rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * Rb.velocity.y;
        }

        cameraControl.PlayerLookAt();

        DoSprint();
        CheckWaterLevel();
        IsGrounded();
        if (!IsGrounded())
        {
            playerAnim.Jumping = false;

        }
    }

    public override void OnStateUpdate()
    {
        
    }
    

    

    private void DoJump(InputAction.CallbackContext obj)
    {

        if (IsGrounded())
        {
            playerAnim.Jumping = true;
            forceDirection += Vector3.up * jumpForce;
            StartCoroutine(jumpCooldown());
            
        }
        else
        {
            playerAnim.Jumping = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(Feet.transform.position, Vector3.down, 1.1f);
    }

    private void CheckWaterLevel()
    {
        if (WaterLevel.InWater)
        {
            WaterLevel.GetWaterLevel();
            //waterLevel.swimLevel = 0f;
            
        }

        if (WaterLevel.Distance_Surface >= WaterLevel.SwimLevel)
        {
            owner.SwitchState(typeof(SwimmingState));
        }
    }

    IEnumerator jumpCooldown()
    {
        yield return new WaitForSeconds(jumpButtonTime);
        playerAnim.Jumping = false;

    }
}
