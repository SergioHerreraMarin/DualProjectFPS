using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawn : MonoBehaviour
{
    private Vector3 spawnPosition;
    private PhotonView spawnPlayer;
    private Spawner spawner;

    private void Start() {
        SetTeamPosition();
        PhotonView spawnPlayer = GameObject.FindWithTag("Spawner").GetComponent<PhotonView>();
        spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
    }


    private void SetTeamPosition(){
        spawnPlayer.RPC("AddSpawnPositionToPlayer", RpcTarget.All);
    }


    


}
