using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

[RequireComponent(typeof(Rigidbody))]
public class WalkingState : BaseState
{
    public WaterLevelCheck waterLevel;
    //LayerMasks
    public LayerMask GroundMask;
    public LayerMask WaterMask;

    //Input fields
    private PlayerManager playerManager;
    private CinemachineFreeLook camera;

    //Movement fields
    [SerializeField] private float moveForce = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxSpeed = 5f;

    private Rigidbody rb;
    private Vector3 forceDirection = Vector3.zero;


    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        camera = GetComponent<PlayerManager>().camera;
        rb = GetComponent<Rigidbody>();
    }
    public override void OnStateEnter()
    {
        Debug.Log("WALK");
        waterLevel.InWater = false;
        playerManager.playerActionsAsset.Player.Jump.started += DoJump;
        rb.drag = 3.5f;
    }


    public override void OnStateExit()
    {
        playerManager.playerActionsAsset.Player.Jump.started -= DoJump;
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

        CheckWaterLevel();
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

    private void DoJump(InputAction.CallbackContext obj)
    {
        //Debug.Log("Jump");

        if (IsGrounded())
        {
            //Debug.Log("Grounded");
            forceDirection += Vector3.up * jumpForce;
        }
        else
        {
            //Debug.Log("NOTGrounded");

        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void CheckWaterLevel()
    {
        if (waterLevel.InWater)
        {
            waterLevel.GetWaterLevel();
        }

        if (waterLevel.Distance_Surface >= waterLevel.swimLevel)
        {
            owner.SwitchState(typeof(SwimmingState));
        }
    }
}
