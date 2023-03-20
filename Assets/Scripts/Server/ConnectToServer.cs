using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class ConnectToServer : MonoBehaviourPunCallbacks
{

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Connectar al servidor de Photon
    }

    public override void OnConnectedToMaster() //Si se ha podido conectar, hacemos join al Lobby. 
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
        Debug.Log("<color=green>Connectado al server</color>");

    }


    public override void OnJoinedLobby() //Si se ha podido hacer join al lobby, cambiamos de escena. 
    {
        base.OnJoinedLobby();
        SceneManager.LoadScene("Lobby");
    }

}
