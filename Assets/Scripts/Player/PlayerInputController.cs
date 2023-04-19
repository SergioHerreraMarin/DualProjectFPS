using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Vector2 playerMovementInput;
    private Vector2 playerLookInput;
    private bool playerJumpInput;
    private bool playerShiftMovementInput;
  
    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
    }

    public Vector2 GetPlayerMovementInput(){
        return playerMovementInput;
    }

    public Vector2 GetPlayerLookInput(){
        return playerLookInput;
    }

    public bool GetPlayerJumpInput(){
        return playerJumpInput;
    }

    public bool GetPlayerShiftMovementInput(){
        return playerShiftMovementInput;
    }

    private void Update() {
        RecibeMovementInput();
        RecibeLookInput();
    }

    private void RecibeMovementInput(){
        playerMovementInput = playerInput.actions["Move"].ReadValue<Vector2>();
    }

    private void RecibeLookInput(){ 
        playerLookInput = playerInput.actions["Look"].ReadValue<Vector2>();
    }


    //Unity Input Events
    public void RecibeJumpInput(InputAction.CallbackContext callbackContext){
        playerJumpInput = callbackContext.performed;
    }

    public void RecibeShiftMovementInput(InputAction.CallbackContext callbackContext){
        if(callbackContext.performed || callbackContext.started){
            playerShiftMovementInput = true;
        }else{
            playerShiftMovementInput = false;
        }; 
    }


}
