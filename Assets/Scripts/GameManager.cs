using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    public static Photon.Realtime.Player[] currentPlayersInRoom;

    
    private void Start() 
    {
        currentPlayersInRoom = PhotonNetwork.PlayerList;
        Debug.Log("Current players in room: " + currentPlayersInRoom.Length);
        for(int i = 0; i < currentPlayersInRoom.Length; i++)
        {
            Debug.Log("Player ID: " + currentPlayersInRoom[i].UserId);
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
