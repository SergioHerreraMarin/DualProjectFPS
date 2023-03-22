using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField createRoomInputField;
    [SerializeField] private TMP_InputField joinRoomInputField;
    [SerializeField] private Button createRoomButton;
    [SerializeField] private Button joinRoomButton;


    private void Awake() {
        createRoomButton.onClick.AddListener(() => CreateRoom());
        joinRoomButton.onClick.AddListener(() => JoinRoom());
    }


    private void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomInputField.text);
    }

    private void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomInputField.text);
    }


    public override void OnJoinedRoom() //Cuando se ha podido unit a una sala. 
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("GamePrueba");
    }


}
