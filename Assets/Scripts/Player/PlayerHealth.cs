using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

[RequireComponent(typeof(PlayerRagdoll))]
public class PlayerHealth : MonoBehaviourPun
{
    [SerializeField] private float timeToResetHealth;
    private const int MAX_HEALTH = 100;
    private float playerHealth;
    private PlayerRagdoll playerRagdoll;
    private bool isDeath;

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
        this.isDeath = false;
    }


    public bool GetIsDeath()
    {   
        return this.isDeath;
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
                //Debug.Log("Jugador muerto:  " + gameObject.name);
                this.isDeath = true;
                playerRagdoll.ActiveRagdoll();
                Invoke("TellMyDeathToGameManager", 1f);
                
                //Update Ppoints --------------------------------------------------------------------
                if(photonView.IsMine)
                {
                    string currentPlayerId = PhotonNetwork.LocalPlayer.UserId;
                    PhotonView gameManagerPhotonView = GameObject.FindWithTag("GameManager").GetComponent<PhotonView>();
                    if(gameManagerPhotonView != null)
                    {
                        gameManagerPhotonView.RPC("UpdatePlayerPoints", RpcTarget.All, currentPlayerId);
                    }
                    /**/

                    MenuMediator.currentUser.AddDeaths(1);
                    
                    /**/
                }
            }

            HealthUpdateEvent(); //Este evento avisará al HUD de que ha recibido daño. 
            //Debug.Log("Damage by: " + info.Sender + ", " + info.photonView);
        }
    }


    private void TellMyDeathToGameManager()
    {
        if(photonView.IsMine) //Para que solo el jugador muerto de uno de los clientes informe y no todos. 
        {
            PhotonView gameManagerPhotonView = GameObject.FindWithTag("GameManager").GetComponent<PhotonView>();
            if(gameManagerPhotonView != null)
            {
                gameManagerPhotonView.RPC("CompleteRound", RpcTarget.All);
            }
        }
    }


    [PunRPC]
    public void ResetHealth()
    {
        Debug.Log("Reset health");
        this.playerHealth = MAX_HEALTH;
        this.isDeath = false;
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
