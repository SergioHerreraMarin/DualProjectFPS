using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{

    [SerializeField] private MenuMediator menuMediator;


    //Callbacks
    public override void OnConnectedToMaster() //Si se ha podido conectar, hacemos join al Lobby. 
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.NickName = menuMediator.getCurrentUser().getUserName();
        Debug.Log("<color=green>Connectado al server</color>");
        Debug.Log("<color=green>Nickname: " + PhotonNetwork.NickName +"</color>");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning("No se ha podido conectar al servidor master, causa: " + cause.ToString());
        menuMediator.ShowMessagePanel("No se ha podido conectar al servidor");
    }


    public override void OnJoinedLobby() //Si se ha podido hacer join al lobby, cambiamos de escena. 
    {
        menuMediator.OpenCreateRoomMenu();
        Debug.Log("<color=green>Conectado al lobby</color>");
    }


    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings(); //Connectar al servidor de Photon
        Debug.Log("<color=green>Conectando...r</color>");
    }


}
