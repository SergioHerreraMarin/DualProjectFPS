using UnityEngine;
using Photon.Pun;

public class PlayerHudInstantiator : MonoBehaviourPun
{
    [SerializeField] private GameObject playerHUDPrefab;

    private void Start() 
    {           
        InstantiatePlayerHUD();
    }

    private void InstantiatePlayerHUD()
    {
        Debug.Log("Instanciando HUD");
        GameObject hudPrefab = Instantiate(this.playerHUDPrefab);
        hudPrefab.GetComponent<PlayerHUD>().setTarget(this.gameObject);
    }

}
