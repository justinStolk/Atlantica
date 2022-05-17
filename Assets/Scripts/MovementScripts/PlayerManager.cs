using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public PlayerInputActions playerActionsAsset;
    public InputAction move;
    public Transform backpack;
    public Transform player;

    public CinemachineFreeLook camera;

    private PlayerInput playerInput;
    private WalkingState walkingState;
    private WaterLevelCheck waterLevelCheck;
    private FSM stateMachine;

    // Start is called before the first frame update
    void Awake()
    {

        playerActionsAsset = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        playerActionsAsset.Player.Enable();
        walkingState = GetComponent<WalkingState>();
        waterLevelCheck = GetComponent<WaterLevelCheck>();
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.SwitchBackPack.canceled += SwitchBackpack;

        //Toggle is Q
        playerActionsAsset.Backpack.TogglePlayerFollow.started += ctx => EventSystem.CallEvent(EventSystem.EventType.ON_BACKPACK_TOGGLED);
        playerActionsAsset.Backpack.TogglePlayerFollow.started += ctx => SwitchPlayer();
        playerActionsAsset.Player.ToggleBackPackFollow.started += ctx => EventSystem.CallEvent(EventSystem.EventType.ON_BACKPACK_TOGGLED);
        playerActionsAsset.Player.ToggleBackPackFollow.started += SwitchBackpack;
    }

    private void Start()
    {
        stateMachine = new FSM(typeof(WalkingState), GetComponents<BaseState>());
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.FSMUpdate();
    }
    private void FixedUpdate()
    {
        stateMachine.FSMFixedUpdate();
    }

    private void SwitchBackpack(InputAction.CallbackContext obj)
    {
        camera.m_Follow = backpack;
        camera.m_LookAt = backpack;

        stateMachine.SwitchState(typeof(BackpackFlyingState));
        playerInput.SwitchCurrentActionMap("Backpack");
    }

    public void SwitchPlayer()
    {
        Debug.Log("Follow player");
        camera.m_Follow = player;
        camera.m_LookAt = player;

        playerInput.SwitchCurrentActionMap("Player");
        
    }
}
