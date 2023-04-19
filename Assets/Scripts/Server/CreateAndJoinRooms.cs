using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField createRoomInputField;
    [SerializeField] private TMP_InputField joinRoomInputField;
    [SerializeField] private Button createRoomButton;
    [SerializeField] private Button joinRoomButton;

    private RoomOptions roomOptions;
    private const int MAX_ROOM_PLAYERS = 2;

    private void Awake() 
    {
        // Esto hace que, todos los jugadores que estén en una sala, al cargar un nivel con PhotonNetwork.LoadLevel, todos los jugadores carguen la misma escena. 
        PhotonNetwork.AutomaticallySyncScene = true;
        roomOptions = new RoomOptions();

        createRoomButton.onClick.AddListener(() => CreateRoom());
        joinRoomButton.onClick.AddListener(() => JoinRoom());
    }

    private void Start() {
        ConfigureRoomOptions();
    }

    private void CreateRoom()
    {   
        PhotonNetwork.CreateRoom(createRoomInputField.text, roomOptions);
    }

    private void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomInputField.text);
    }


    //Cuando se ha podido unit a una sala. 
    public override void OnJoinedRoom() 
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("<color=yellow>Unido a la sala con éxito :D </color>");
        }else
        {
            Debug.Log("<color=yellow>Unido a la sala con éxito :D esperando 1 jugador... </color>");
        }   
    }

    
    //Si ya estás unido a la sala y entra otro jugador, se ejecuta este. 
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("GamePrueba");
        }
    }

    
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("No se ha podido unir a la sala " + joinRoomInputField.text);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("No se ha podido crear la sala " + joinRoomInputField.text);
    }

    private void ConfigureRoomOptions()
    {
        roomOptions.MaxPlayers = MAX_ROOM_PLAYERS;
    }

}
