using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{

    public List<GameObject> playersInCurrentRoom = new List<GameObject>();
    

    private void Start() 
    {
        
    }

    [PunRPC]
    public void AddPlayersToList(GameObject player){
        playersInCurrentRoom.Add(player);

        Debug.Log("Players: ==================================");
        foreach(GameObject currentPlayer in playersInCurrentRoom)
        {
            Debug.Log("Player: " + currentPlayer.name);
        }
    }


    /*public void AddNewPlayer(GameObject player)
    {
        currentPlayersInRoom.Add(player);
    }

    public List<GameObject> GetCurrentPlayersInRoom()
    {
        return this.currentPlayersInRoom;
    }*/


}
