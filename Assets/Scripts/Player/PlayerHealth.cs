using UnityEngine;

[RequireComponent(typeof(PlayerRagdoll))]
public class PlayerHealth : MonoBehaviour
{
    private const int MAX_HEALTH = 100;
    private float playerHealth;
    private PlayerRagdoll playerRagdoll;

    private void Awake() {
        playerRagdoll = GetComponent<PlayerRagdoll>();
    }

    private void Start() {
        playerHealth = MAX_HEALTH;
    }

    public void SetDamage(float damage){
        
        if(playerHealth > 0)
        {
            playerHealth -= damage;

            if(playerHealth <= 0){
                playerHealth = 0;
                playerRagdoll.ActiveRagdoll();
            }
        }
 
    }

}
