using UnityEngine;
using Photon.Pun;


public class Spawner : MonoBehaviourPun 
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] private Transform spawnA;
    [SerializeField] private Transform spawnB;
    private int indexToSpawn;
    private bool repeatSpawnRandomIndex;
  
    private void Start() {
        SpawnPlayer();    
    }


    private void SpawnPlayer(){
        GameObject player;
        if(PhotonNetwork.IsMasterClient){
            player = PhotonNetwork.Instantiate(playerPrefab.name, spawnA.position, spawnA.rotation);
            Debug.Log("Spawn cliente maestro");
        }else{
            player = PhotonNetwork.Instantiate(playerPrefab.name, spawnB.position, spawnB.rotation);
            Debug.Log("Spawn no cliente maestro");
        }
    }

    
}
