using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{

    [SerializeField] private MenuMediator menuMediator;
    private bool connectedToServer;
    

    private void Start() {
        connectedToServer = false;
    }

    public bool GetConnectedToServer()
    {
        return this.connectedToServer;
    }


    //Callbacks
    public override void OnConnectedToMaster() //Si se ha podido conectar, hacemos join al Lobby. 
    {
        connectedToServer = true;
        PhotonNetwork.JoinLobby();
        Debug.Log("<color=green>Connectado al server</color>");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectedToServer = false;
        Debug.LogWarning("No se ha podido conectar al servidor master, causa: " + cause.ToString());
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
