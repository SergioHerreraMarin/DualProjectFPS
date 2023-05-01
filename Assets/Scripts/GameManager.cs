using ExitGames.Client.Photon;
using System.Collections;
using Photon.Realtime;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private int rounds;
    [SerializeField] private GameObject wallA;
    [SerializeField] private GameObject wallB;
    [SerializeField] private int timeToActiveRound;
    private bool inRound;
    private int currentRound;
    private PhotonView photonViewWallA;
    private PhotonView photonViewWallB;
    private GameObject clientPlayer;

    private const int INITIAL_PLAYER_SCORE = 3;


    //EVENTS
    public delegate void FinishGameDelegate();
    public event FinishGameDelegate FinishGameEvent;
    public delegate void ChangeRoundDelegate();
    public event ChangeRoundDelegate ChangeRoundEvent;
    public delegate void UpdateMasterClientPoints();
    public event UpdateMasterClientPoints UpdateMasterClientPointsEvent;

    public void SetPlayer(GameObject clientPlayer)
    {
        this.clientPlayer = clientPlayer;
        Debug.Log("Set client player");
    }


    public int GetRounds()
    {
        return this.rounds;
    }

    public int GetCurrentRound()
    {
        return this.currentRound;
    }

    public int GetPlayerPoints()
    {
        return 0;
    }

    private void Start() 
    {
        if(PhotonNetwork.IsMasterClient)
        {
            photonViewWallA = wallA.GetComponent<PhotonView>();
            photonViewWallB = wallB.GetComponent<PhotonView>();
            ActiveWalls();

            

        }

        StartCoroutine(Prueba());

        inRound = false;
        currentRound = 0;
        Invoke("ActiveRound", timeToActiveRound);

    }


    IEnumerator Prueba()
    {
        yield return new WaitForSeconds(1.0f);

        Debug.Log("Current users: " + PhotonNetwork.PlayerList.Length);

        //El master client asigna una puntuación inicial a todos los jugadores de la sala. 
        ExitGames.Client.Photon.Hashtable roomProperties = new ExitGames.Client.Photon.Hashtable();

        foreach(Player player in PhotonNetwork.PlayerList)
        {
            Debug.Log("Añadido: " + player.UserId + ", " + INITIAL_PLAYER_SCORE);
            roomProperties.Add(player.UserId, INITIAL_PLAYER_SCORE);
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
    }


    private void ActiveRound()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            DesactiveWalls();
        }    
        
        inRound = true;
        currentRound++;
        ChangeRoundEvent();
        Debug.Log("<color=orange>In round " + currentRound + "/" + rounds + "</color>");
        
    }


    [PunRPC]
    private void ResetRound() //Llamado desde un jugador cuando muere. 
    {   
        if(currentRound < rounds)
        {
            //Cada uno en su cliente. 
            if(clientPlayer == null)
            {
                Debug.LogError("No se encuentra el player refernecia");
            }

            PhotonView player = clientPlayer.GetComponent<PhotonView>();
            if(player != null)
            {
                Debug.Log("Reset health 2");
                player.RPC("ResetHealth", RpcTarget.All);
                player.RPC("ResetPositionAndRotation", RpcTarget.All);
            }else
            {
                Debug.LogError("No se encuentra el player");
            }

            if(PhotonNetwork.IsMasterClient)
            {
                ActiveWalls();
            }
            
            Invoke("ActiveRound", timeToActiveRound);
            inRound = false;            
            

        }else
        {
            FinishGame();
        }
    }


    private void DesactiveWalls()
    {
        photonViewWallA.RPC("DesactiveWalls", RpcTarget.All);
        photonViewWallB.RPC("DesactiveWalls", RpcTarget.All);
    }

    private void ActiveWalls()
    {
        photonViewWallA.RPC("ActiveWalls", RpcTarget.All);
        photonViewWallB.RPC("ActiveWalls", RpcTarget.All);
    }


    private void FinishGame()
    {   
        FinishGameEvent();
        Debug.Log("<color=yellow>Finish Game</color>");
    }


    [PunRPC]
    public void UpdatePlayerPoints(string playerId)
    {
        ExitGames.Client.Photon.Hashtable roomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        Debug.Log("<color=red>PUNTOSGM: " + ((int)roomProperties[playerId] - 1) + "</color>");
        roomProperties[playerId] = (int)roomProperties[playerId] - 1;
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
        UpdateMasterClientPointsEvent();

        //Log
        foreach(Player player in PhotonNetwork.PlayerList)
        {   
            ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.CurrentRoom.CustomProperties;
            int playerScore = (int)hash[player.UserId];
            Debug.Log("ID PLAYER: " + player.UserId + ", POINTS: " + playerScore);
        }
    
    }


    //PUN CALLBACKS
    public override void OnLeftRoom()
    {   
        Debug.Log("<color=yellow>Left room</color>");
        SceneManager.LoadScene(0);
    }


}
