using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PlayerRagdoll))]
public class PlayerHealth : MonoBehaviourPun
{
    private const int MAX_HEALTH = 100;
    private float playerHealth;
    private PlayerRagdoll playerRagdoll;


    //EVENTS
    public delegate void HealthUpdateDelegate();
    public event HealthUpdateDelegate HealthUpdateEvent;

    private void Awake()
    {
        playerRagdoll = GetComponent<PlayerRagdoll>();
    }

    private void Start()
    {
        this.playerHealth = MAX_HEALTH;
    }

    //Este método es llamado desde weapon para dañar al jugador. 
    //El atributo [PunRPC] es para que se pueda llamar de forma remota a través de la red de Photon. 
    [PunRPC]
    public void TakeDamage(float damage, PhotonMessageInfo info)
    {
        if(this.playerHealth > 0)
        {
            this.playerHealth -= damage;

            if(this.playerHealth <= 0)
            {
                this.playerHealth = 0;
                Debug.Log("Jugador muerto:  " + gameObject.name);
                playerRagdoll.ActiveRagdoll();

                Invoke("InvokeResetHealth", 5); //Chapuza
            }

            HealthUpdateEvent(); //Este evento avisará al HUD de que ha recibido daño. 
            Debug.Log("Damage by: " + info.Sender + ", " + info.photonView);
        }
    }


    private void InvokeResetHealth()
    {
        photonView.RPC("ResetHealth", RpcTarget.All);
    }


    [PunRPC]
    public void ResetHealth()
    {
        this.playerHealth = MAX_HEALTH;
        playerRagdoll.DesactiveRagdoll();
        HealthUpdateEvent();
    }


    public float GetHealth()
    {
        return this.playerHealth;
    }

    public int GetMaxHealth()
    {
        return MAX_HEALTH;
    }

}
