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
    private bool playerShootInput;

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

    public bool GetPlayerShootInput(){
        return playerShootInput;
    }

    private void Update() {
        RecibeMovementInput();
        RecibeLookInput();
        RecibeShootInput(); //No deberia estar en update.. xd
    }

    private void RecibeMovementInput(){
        playerMovementInput = playerInput.actions["Move"].ReadValue<Vector2>();
        //Debug.Log("Movement x: " + playerMovementInput.x + ", Movement y: " + playerMovementInput.y);
    }

    private void RecibeLookInput(){ //No funcina con el action por algun motivo xd
        //playerLookInput = playerInput.actions["Look"].ReadValue<Vector2>();
        playerLookInput = Mouse.current.delta.ReadValue();
        //Debug.Log(playerLookInput);
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

    //No funcina con el action por algun motivo xd
    public void RecibeShootInput(){
        float leftButtonValue = Mouse.current.leftButton.ReadValue(); //Chapuza hasta solucionarlo 
        if(leftButtonValue == 0){
            playerShootInput = false;
        }else{
            playerShootInput = true;
        }
    }

}
