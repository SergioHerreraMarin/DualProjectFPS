using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player stats")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerShiftSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpRayLength;
    [SerializeField] private float playerLookSpeed;
    [Space(10)]
    [Header("References")]
    [SerializeField] private Camera cam;

    private PlayerInputController playerInputController;
    private Transform playerTransform;
    private float verticalLookValue = 0;
    private Ray jumpRay;
    private Vector3 jumpDirection;
    private float jumpSpeed;
    private float speed;

    private void Awake() {
        playerInputController = GetComponent<PlayerInputController>();
        playerTransform = GetComponent<Transform>();
    }


    private void Update() {
        Movement();
        ShiftMovement();
        Look();
        Jump();
    }


    private void Movement(){
        playerTransform.Translate((new Vector3(playerInputController.GetPlayerMovementInput().x,
        0,playerInputController.GetPlayerMovementInput().y) * speed) * Time.deltaTime); 
    }

    private void ShiftMovement(){
        if(playerInputController.GetPlayerShiftMovementInput()){
            speed = playerShiftSpeed;
        }else{
            speed = playerSpeed;
        }
    }

    private void Look(){
        //Rotate player Y
        playerTransform.Rotate(new Vector3(0,playerInputController.GetPlayerLookInput().x * playerLookSpeed * Time.deltaTime,0), Space.Self);
        //Rotate player X
        //Valor obtenido el input del ratón, se le multiplica - 1 para invertirlo y se le multiplica por un valor de velocidad. 
        verticalLookValue += (playerInputController.GetPlayerLookInput().y * -1) * playerLookSpeed * Time.deltaTime;
        //Clamp del valor de rotación, minimo de -90º y máximo de 90º
        verticalLookValue = Mathf.Clamp(verticalLookValue, -75, 75);
        //asigna directamente un angulo de rotación en grados en el eje de las X del transform de la cámara en local. 
        cam.transform.localEulerAngles = new Vector3(verticalLookValue,cam.transform.localEulerAngles.y,cam.transform.localEulerAngles.z);
    }

    private void Jump(){

        jumpRay = new Ray(transform.position, Vector3.down);

        if(Physics.Raycast(jumpRay, jumpRayLength)){ //Si detecta suelo.. 
            if(playerInputController.GetPlayerJumpInput()){ //Si se activa el evento de salto 
                jumpDirection.y = jumpForce;
            }else{
                jumpDirection.y = 0;
            }
        }else{
            jumpDirection.y -= gravity * Time.deltaTime; //Si no detecta suelo, disminuye la posición en las Y. 
        }

        playerTransform.Translate(jumpDirection, Space.Self);
        Debug.DrawRay(jumpRay.origin,jumpRay.direction * jumpRayLength, Color.green);
    }

}
