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

    private void Awake() {
        createRoomButton.onClick.AddListener(() => CreateRoom());
        joinRoomButton.onClick.AddListener(() => JoinRoom());
        roomOptions = new RoomOptions();
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


    public override void OnJoinedRoom() //Cuando se ha podido unit a una sala. 
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");
        PhotonNetwork.LoadLevel("GamePrueba");
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
