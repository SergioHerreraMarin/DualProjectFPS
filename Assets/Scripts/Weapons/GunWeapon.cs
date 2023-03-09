using UnityEngine;

public class GunWeapon : MonoBehaviour, IShoot
{
    [Header("WEAPON STATS")]
    [SerializeField] private LayerMask weaponLayerMask;
    [SerializeField] private float weaponRayLength;
    [SerializeField] private float shootCoolDown;
    [SerializeField] private float shootDamage;
    
    [Space(10)]
    [Header("REFERENCES")]
    [SerializeField] private PlayerInputController playerInputController;
    [SerializeField] private Camera playerCamera;

    private RaycastHit rayHit;
    private Ray weaponRay;

    private float currentTime;
    private float timeThreshold = 0;


    private void Update() {    
        Shoot();
    }

    public void Shoot(){
        weaponRay = playerCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        currentTime += Time.deltaTime; //Se va sumando cada segundo. 

        if(playerInputController.GetPlayerShootInput()){ 

            if(currentTime > timeThreshold){
                if(Physics.Raycast(weaponRay, out rayHit ,weaponRayLength, weaponLayerMask, QueryTriggerInteraction.Ignore)){
                    Debug.DrawRay(weaponRay.origin, weaponRay.direction * weaponRayLength, Color.green);
                    Debug.Log(rayHit.transform.name);
                }else{
                    Debug.DrawRay(weaponRay.origin, weaponRay.direction * weaponRayLength, Color.yellow);
                }
                timeThreshold = currentTime + shootCoolDown;
            }           
        }
    }
}
