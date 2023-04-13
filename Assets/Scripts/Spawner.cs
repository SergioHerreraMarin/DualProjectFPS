using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviourPun 
{

    [SerializeField]
    private Transform[] spawnPositions;
    private Dictionary<int, bool> positionsDic = new Dictionary<int, bool>();
    private int indexToSpawn;
    private bool repeatSpawnRandomIndex;


    private void Start() {
        for(int i = 0; i < spawnPositions.Length; i++)
        {
            positionsDic.Add(i, true);
        }  
    }


    [PunRPC]
    public void AddSpawnPositionToPlayer(){ //Devuelve una posición disponible entre las posibles. 

        Vector3 position = Vector3.zero;

        do{
            repeatSpawnRandomIndex = true;
            indexToSpawn = Random.Range(0, spawnPositions.Length); // 0
            
            if(positionsDic[indexToSpawn])
            {
                // GameObject player;
                // player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPositions[indexToSpawn].position, Quaternion.identity);
                position = spawnPositions[indexToSpawn].position;
              
                positionsDic[indexToSpawn] = false;
                repeatSpawnRandomIndex = false;
                Debug.Log("Spawn at point: " + indexToSpawn);
            }
        
        }while(repeatSpawnRandomIndex);

    }



}
