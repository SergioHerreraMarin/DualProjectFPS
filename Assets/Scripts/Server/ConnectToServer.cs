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
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
        Debug.Log("<color=green>Connectado al server</color>");
    }

    public override void OnErrorInfo(ErrorInfo errorInfo)
    {
        base.OnErrorInfo(errorInfo);
        Debug.Log(errorInfo.Info);
    }

    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        base.OnCustomAuthenticationFailed(debugMessage);
        Debug.Log(debugMessage);
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log(cause.ToString());
    }


    public override void OnJoinedLobby() //Si se ha podido hacer join al lobby, cambiamos de escena. 
    {
        base.OnJoinedLobby();
        SceneManager.LoadScene("Lobby");
    }

}
