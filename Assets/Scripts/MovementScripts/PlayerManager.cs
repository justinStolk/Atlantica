using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public PlayerInputActions playerActionsAsset;
    public InputAction move;

    private PlayerInput playerInput;
    private WalkingState walkingState;

    // Start is called before the first frame update
    void Awake()
    {
        playerActionsAsset = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        playerActionsAsset.Player.Enable();
        walkingState = GetComponent<WalkingState>();

        move = playerActionsAsset.Player.Move;
        //playerActionsAsset.Player.SwitchBackPack.started += SwitchBackpack;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerInput.currentActionMap);
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Disable");
            playerActionsAsset.Player.Disable();
            Debug.Log("switch map");
            playerInput.SwitchCurrentActionMap("Backpack");
            Debug.Log("switch state");
            walkingState.SwitchState();
            Debug.Log("Complete");
        }
    }



    //private void SwitchBackpack(InputAction.CallbackContext obj)
    //{
    //    Debug.Log("Disable");
    //    playerActionsAsset.Player.Disable();
    //    Debug.Log("switch map");
    //    playerInput.SwitchCurrentActionMap("Backpack");
    //    Debug.Log("switch state");
    //    walkingState.SwitchState();
    //    Debug.Log("Complete");
    //}
}
