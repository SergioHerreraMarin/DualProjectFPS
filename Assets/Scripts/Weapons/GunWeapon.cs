using UnityEngine;
using UnityEngine.InputSystem;

public class GunWeapon : MonoBehaviour, IShoot
{
    [Header("WEAPON STATS")]
    [SerializeField] private LayerMask weaponLayerMask;
    [SerializeField] private float weaponRayLength;
    [SerializeField] private float shootDamage;
    
    [Space(10)]
    [Header("REFERENCES")]
    [SerializeField] private Camera playerCamera;

    private RaycastHit rayHit;
    private Ray weaponRay;

    public void Shoot(InputAction.CallbackContext callbackContext)
    {

        if(callbackContext.started)
        {
            weaponRay = playerCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0));

            Debug.Log("Entra");
            if(Physics.Raycast(weaponRay, out rayHit ,weaponRayLength, weaponLayerMask, QueryTriggerInteraction.Ignore))
            {
                Debug.DrawRay(weaponRay.origin, weaponRay.direction * weaponRayLength, Color.green);
                Debug.Log(rayHit.transform.name);
            }else
            {
                Debug.DrawRay(weaponRay.origin, weaponRay.direction * weaponRayLength, Color.red);
            }
        }
    }



}
