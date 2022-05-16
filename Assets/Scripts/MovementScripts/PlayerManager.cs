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
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerInput.currentActionMap);
        
    }

    private void SwitchBackpack(InputAction.CallbackContext obj)
    {
        camera.m_Follow = backpack;
        camera.m_LookAt = backpack;

        walkingState.SwitchState();
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
