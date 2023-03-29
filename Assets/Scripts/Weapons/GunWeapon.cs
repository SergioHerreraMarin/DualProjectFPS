using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class GunWeapon : MonoBehaviourPun, IShoot
{
    [Header("WEAPON STATS")]
    [SerializeField] private LayerMask weaponLayerMask;
    [SerializeField] private float weaponRayLength;
    [SerializeField] private float shootDamage;
    
    [Space(10)]
    [Header("REFERENCES")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject muzzleFlashVfx;
    [SerializeField] private Animator anim;

    private RaycastHit rayHit;
    private Ray weaponRay;

    private ParticleSystem[] muzzleFlashParticles;


    private void Start() {
        muzzleFlashParticles = muzzleFlashVfx.GetComponentsInChildren<ParticleSystem>();
    }

    public void Shoot(InputAction.CallbackContext callbackContext)
    {
        if(photonView.IsMine)  //Si es mi instancia de prefab cliente, dispara.
        {
            if(callbackContext.started)
            {   
                weaponRay = playerCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0));

                PlayMuzzleGun();

                if(Physics.Raycast(weaponRay, out rayHit ,weaponRayLength, weaponLayerMask, QueryTriggerInteraction.Ignore))
                {
                    if(rayHit.transform.gameObject.tag == "Player"){
                        PlayerHealth playerHealth = rayHit.transform.gameObject.GetComponentInParent<PlayerHealth>();
                        playerHealth.SetDamage(25);
                        Debug.Log("Set damage to " + rayHit.transform.gameObject.name);
                    }

                    Debug.DrawRay(weaponRay.origin, weaponRay.direction * weaponRayLength, Color.green);
                    Debug.Log(rayHit.transform.name);
                }else
                {
                    Debug.DrawRay(weaponRay.origin, weaponRay.direction * weaponRayLength, Color.red);
                }
            }
        }
  
    }


    private void PlayMuzzleGun(){
        foreach(ParticleSystem particle in muzzleFlashParticles){
            particle.Play();
        }
    }


}
