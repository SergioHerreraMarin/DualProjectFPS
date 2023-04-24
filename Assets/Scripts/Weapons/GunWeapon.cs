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
    [SerializeField] private GameObject bulletImpactVfx;
    [SerializeField] private GameObject bloodImpactVfx;
    [SerializeField] private Animator anim;

    private RaycastHit rayHit;
    private Ray weaponRay;

    private ParticleSystem[] muzzleFlashParticles;
    private ParticleSystem[] bulletImpactParticles;


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
                anim.SetTrigger("Shoot");
                PlayMuzzleGun();

                if(Physics.Raycast(weaponRay, out rayHit ,weaponRayLength, weaponLayerMask, QueryTriggerInteraction.Ignore))
                {
                    if(rayHit.transform.gameObject.tag == "Player"){

                        //Con RPC se puede llamar a un método en un objeto remoto de la red de Photon.
                        PhotonView otherPlayer = rayHit.transform.gameObject.GetComponentInParent<PhotonView>();
                        otherPlayer.RPC("TakeDamage", RpcTarget.All, shootDamage);
                        Debug.Log("Set damage to " + rayHit.transform.gameObject.name);
                        InstantiateBloodParticles();

                    }else
                    {
                        InstantiateBulletImpactParticles();
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


    private void PlayMuzzleGun()
    {
        foreach(ParticleSystem particle in muzzleFlashParticles){
            particle.Play();
        }
    }

    private void InstantiateBulletImpactParticles()
    {
        GameObject impactParticles = Instantiate(bulletImpactVfx, rayHit.point, Quaternion.FromToRotation(Vector3.forward, rayHit.normal));
    }

    private void InstantiateBloodParticles()
    {
        GameObject bloodParticles = Instantiate(bloodImpactVfx, rayHit.point, Quaternion.FromToRotation(Vector3.forward, rayHit.normal));
    }


}
