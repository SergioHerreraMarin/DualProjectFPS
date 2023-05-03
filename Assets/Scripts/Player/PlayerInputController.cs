using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private CollisionDetection collisionDetection;
    private PlayerInput playerInput;
    private Vector2 playerMovementInput;
    private Vector2 playerLookInput;
    private bool playerJumpInput;
    private bool playerShiftMovementInput;
  
    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
    }

    public Vector2 GetPlayerMovementInput(){

        if(!collisionDetection.GetCanMoveW())
        {
            return new Vector2(playerMovementInput.x, Mathf.Clamp(playerMovementInput.y, -1, 0));
        }

        if(!collisionDetection.GetCanMoveS())
        {
            return new Vector2(playerMovementInput.x, Mathf.Clamp(playerMovementInput.y, 0, 1));
        }

        if(!collisionDetection.GetCanMoveD())
        {
            return new Vector2(Mathf.Clamp(playerMovementInput.x, -1, 0), playerMovementInput.y );
        }

        if(!collisionDetection.GetCanMoveA())
        {
            return new Vector2(Mathf.Clamp(playerMovementInput.x, 0, 1), playerMovementInput.y );
        }

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
