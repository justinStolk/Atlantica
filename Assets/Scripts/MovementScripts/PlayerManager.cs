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
    void Start()
    {
        playerActionsAsset = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        walkingState = GetComponent<WalkingState>();

        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Enable();
        playerActionsAsset.Player.SwitchBackPack.started += SwitchBackpack;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerInput.currentActionMap);
    }

    private void SwitchBackpack(InputAction.CallbackContext obj)
    {
        playerActionsAsset.Player.Disable();
        playerInput.SwitchCurrentActionMap("Backpack");
        walkingState.SwitchState();
    }
}
