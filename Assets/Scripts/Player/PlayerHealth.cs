using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PlayerRagdoll))]
public class PlayerHealth : MonoBehaviour, IPunObservable
{
    private const int MAX_HEALTH = 100;
    private float playerHealth;
    private PlayerRagdoll playerRagdoll;

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
    public void TakeDamage(float damage)
    {
        if(this.playerHealth > 0)
        {
            this.playerHealth -= damage;

            if(this.playerHealth <= 0)
            {
                this.playerHealth = 0;
                Debug.Log("Jugador muerto:  " + gameObject.name);
                playerRagdoll.ActiveRagdoll();
            }

            Debug.Log("Take damage. CurrentHealth: " + this.playerHealth);
        }
    }

    public float GetHealth()
    {
        return this.playerHealth;
    }

    public int GetMaxHealth()
    {
        return MAX_HEALTH;
    }

    //Para actualizar nuestra vida al resto de players y al revés. 
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {   
        if(stream.IsWriting){
            stream.SendNext(this.playerHealth);
        }else
        {
            this.playerHealth = (float)stream.ReceiveNext();
        }
    }

}
