using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPositions;
    private Dictionary<int, bool> positionsDic = new Dictionary<int, bool>();
    private int indexToSpawn;
    private bool repeatSpawnRandomIndex;
    

    private void Start() {

        for(int i = 0; i < spawnPositions.Length; i++)
        {
            positionsDic.Add(i, true);
            Debug.Log(positionsDic[i]);
        }

        SpawnPlayers();    
    }


    private void SpawnPlayers()
    {
        do{
            repeatSpawnRandomIndex = true;
            indexToSpawn = Random.Range(0, spawnPositions.Length); // 0
            
            if(positionsDic[indexToSpawn])
            {
                GameObject player;
                player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPositions[indexToSpawn].position, Quaternion.identity);
                PhotonView gameManagerRef = GameObject.FindWithTag("GameManager").GetComponent<PhotonView>();
                gameManagerRef.RPC("AddPlayersToList", RpcTarget.All, player);
                positionsDic[indexToSpawn] = false;
                repeatSpawnRandomIndex = false;
                Debug.Log("Spawn at point: " + indexToSpawn);
            }

        }while(repeatSpawnRandomIndex);
    }



}
