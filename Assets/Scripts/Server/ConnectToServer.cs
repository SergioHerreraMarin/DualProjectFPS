using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Connectar al servidor de Photon
        Debug.Log("<color=green>Conectando...r</color>");
    }

    public override void OnConnectedToMaster() //Si se ha podido conectar, hacemos join al Lobby. 
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("<color=green>Connectado al server</color>");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning("No se ha podido conectar al servidor master, causa: " + cause.ToString());
    }


    public override void OnJoinedLobby() //Si se ha podido hacer join al lobby, cambiamos de escena. 
    {
        Debug.Log("<color=green>Conectado al lobby</color>");
        SceneManager.LoadScene("Lobby");
    }

}
