using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System;

public class PlayerManager : MonoBehaviour
{
    public PlayerInputActions playerActionsAsset;
    public InputAction move;
    public Transform backpack;
    public Transform player;
    public Transform cameraLook;
    //public BackpackFollow backpackFollow;

    public CinemachineFreeLook camera;

    private PlayerInput playerInput;
    private WalkingState walkingState;
    private WaterLevelCheck waterLevelCheck;
    private FSM stateMachine;
    private PlayerInteract playerInteract;

    // Start is called before the first frame update
    void Awake()
    {
        playerActionsAsset = new PlayerInputActions();
        playerActionsAsset.Player.Enable();

        playerInput = GetComponent<PlayerInput>();
        walkingState = GetComponent<WalkingState>();
        waterLevelCheck = GetComponent<WaterLevelCheck>();
        playerInteract = GetComponent<PlayerInteract>();

        move = playerActionsAsset.Player.Move;

        playerActionsAsset.Player.SwitchToBackPack.canceled += SwitchToBackpack;
        playerActionsAsset.Backpack.SwitchToPlayer.started += SwitchToPlayer;        
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

    //ON PRESS R, TOGGLES VIEW AND MOVEMENT TO BACKPACK
    private void SwitchToBackpack(InputAction.CallbackContext obj)
    {
        camera.m_Follow = backpack;
        camera.m_LookAt = backpack;
        cameraLook.transform.SetParent(backpack);
        cameraLook.transform.position = new Vector3(backpack.transform.position.x, backpack.transform.position.y + 1, backpack.transform.position.z);
        EventSystem.CallEvent(EventSystem.EventType.ON_BACKPACK_RELEASE);
        playerInteract.PlayerActive = false;
        stateMachine.SwitchState(typeof(BackpackFlyingState));
        playerInput.SwitchCurrentActionMap("Backpack");
        Debug.Log("GA NAAR BACKPACK");
        
    }

    //ON PRESS R, TOGGLES VIEW AND MOVEMENT TO PLAYER
    private void SwitchToPlayer(InputAction.CallbackContext obj)
    {
        cameraLook.transform.SetParent(player);
        cameraLook.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);

        if (waterLevelCheck.InWater == true)
        {
            stateMachine.SwitchState(typeof(SwimmingState));
        }
        else
        {
            stateMachine.SwitchState(typeof(WalkingState));
        }

        playerInteract.PlayerActive = true;

        SwitchPlayerView();
    }

    public void SwitchPlayerView()
    {
        camera.m_Follow = player;
        camera.m_LookAt = player;

        playerInput.SwitchCurrentActionMap("Player");
        //EventSystem.CallEvent(EventSystem.EventType.ON_PLAYER_VIEW);
    }


}
