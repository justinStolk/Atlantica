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
    public GameObject backpack;
    public Transform player;
    public Transform cameraLook;
    //public BackpackFollow backpackFollow;

    public CinemachineFreeLook camera;

    private PlayerAnimationManager playerAnim;
    private UpgradeBackpack upgradeBackpack;
    private PlayerInput playerInput;
    private WalkingState walkingState;
    private WaterLevelCheck waterLevelCheck;
    private FSM stateMachine;
    public PlayerInteract playerInteract;

    // Start is called before the first frame update
    void Awake()
    {

        playerActionsAsset = new PlayerInputActions();
        playerActionsAsset.Player.Enable();
        stateMachine = new FSM(typeof(WalkingState), GetComponents<BaseState>());
        walkingState = GetComponent<WalkingState>();
    }


    private void Start()
    {

        playerInput = GetComponent<PlayerInput>();
        upgradeBackpack = backpack.GetComponent<UpgradeBackpack>();
        waterLevelCheck = GetComponent<WaterLevelCheck>();
        playerInteract = GetComponent<PlayerInteract>();
        playerAnim = GetComponent<PlayerAnimationManager>();

        move = playerActionsAsset.Player.Move;

        playerActionsAsset.Player.SwitchToBackPack.canceled += SwitchToBackpack;
        playerActionsAsset.Backpack.SwitchToPlayer.started += SwitchToPlayer;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
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
        if (upgradeBackpack.upgraded == true)
        {
            camera.m_Follow = backpack.transform;
            camera.m_LookAt = backpack.transform;
            cameraLook.transform.SetParent(backpack.transform);
            cameraLook.transform.position = new Vector3(backpack.transform.position.x, backpack.transform.position.y + 1, backpack.transform.position.z);
            EventSystem.CallEvent(EventSystem.EventType.ON_BACKPACK_RELEASE);
            playerInteract.PlayerActive = false;
            playerInteract.InteractText.gameObject.SetActive(false);

            stateMachine.SwitchState(typeof(BackpackFlyingState));
            playerInput.SwitchCurrentActionMap("Backpack");
            Debug.Log("GA NAAR BACKPACK");
        }
        else
        {
            Debug.Log("BackPack not yet upgraded");
        }
        
    }

    //ON PRESS R, TOGGLES VIEW AND MOVEMENT TO PLAYER
    public void SwitchToPlayer(InputAction.CallbackContext obj)
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
