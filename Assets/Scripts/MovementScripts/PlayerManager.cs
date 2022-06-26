using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System;

public class PlayerManager : MonoBehaviour
{
    public PlayerInputActions PlayerActionsAsset;
    public InputAction Move;
    public GameObject Backpack;
    public Transform Player;
    public Transform CameraLook;
    public CinemachineFreeLook Camera;
    public PlayerInteract PlayerInteract;


    private PlayerAnimationManager playerAnim;
    private UpgradeBackpack upgradeBackpack;
    private PlayerInput playerInput;
    private WalkingState walkingState;
    private WaterLevelCheck waterLevelCheck;
    private FSM stateMachine;

    // Start is called before the first frame update
    void Awake()
    {

        PlayerActionsAsset = new PlayerInputActions();
        PlayerActionsAsset.Player.Enable();
        stateMachine = new FSM(typeof(WalkingState), GetComponents<BaseState>());
        walkingState = GetComponent<WalkingState>();
    }


    private void Start()
    {

        playerInput = GetComponent<PlayerInput>();
        upgradeBackpack = Backpack.GetComponent<UpgradeBackpack>();
        waterLevelCheck = GetComponent<WaterLevelCheck>();
        PlayerInteract = GetComponent<PlayerInteract>();
        playerAnim = GetComponent<PlayerAnimationManager>();

        Move = PlayerActionsAsset.Player.Move;

        PlayerActionsAsset.Player.SwitchToBackPack.canceled += SwitchToBackpack;
        PlayerActionsAsset.Backpack.SwitchToPlayer.started += SwitchToPlayer;

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
        if (upgradeBackpack.Upgraded == true)
        {
            Camera.m_Follow = Backpack.transform;
            Camera.m_LookAt = Backpack.transform;
            CameraLook.transform.SetParent(Backpack.transform);
            CameraLook.transform.position = new Vector3(Backpack.transform.position.x, Backpack.transform.position.y + 1, Backpack.transform.position.z);
            EventSystem.CallEvent(EventSystem.EventType.ON_BACKPACK_RELEASE);
            PlayerInteract.PlayerActive = false;
            PlayerInteract.InteractText.gameObject.SetActive(false);

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
        CameraLook.transform.SetParent(Player);
        CameraLook.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);

        if (waterLevelCheck.InWater == true)
        {
            stateMachine.SwitchState(typeof(SwimmingState));
        }
        else
        {
            stateMachine.SwitchState(typeof(WalkingState));
        }

        PlayerInteract.PlayerActive = true;

        SwitchPlayerView();
    }

    public void SwitchPlayerView()
    {
        Camera.m_Follow = Player;
        Camera.m_LookAt = Player;
        
        playerInput.SwitchCurrentActionMap("Player");
        //EventSystem.CallEvent(EventSystem.EventType.ON_PLAYER_VIEW);
    }


}
