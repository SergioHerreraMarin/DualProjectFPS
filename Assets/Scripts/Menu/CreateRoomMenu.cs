using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateRoomMenu : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_InputField createRoomInput;
    [SerializeField] private TMP_InputField searchRoomInput;
    [SerializeField] private Button createRoomButton;
    [SerializeField] private Button searchRoomButton;
    [SerializeField] private CanvasGroup canvasGroup;
    private MenuMediator mediator;

    private string createRoomEntered ="";
    private string searchRoomEntered="";

    private void Awake()
    {
        backButton.onClick.AddListener(() => mediator.BackToMainMenu());
        createRoomButton.onClick.AddListener(CreateRoom);
        searchRoomButton.onClick.AddListener(SearchRoom);
    }

    private void CreateRoom(){
        string createRoomEntered = createRoomInput.text;
        mediator.NewRoom(createRoomEntered);
        Debug.Log("CreateRoomMenu: CreateRoom createdRoom "+createRoomEntered);
    }

    private void SearchRoom(){
        string searchRoomEntered = searchRoomInput.text;
        mediator.JoinRoom(searchRoomEntered);
        Debug.Log("CreateRoomMenu: SearchRoom room to search: "+searchRoomEntered);
    }

    public void SetCreateRoomEntered(string createRoom)
    {
        createRoomEntered = createRoom;
        Debug.Log("CreateRoomMenu: SetCreateRoomEntered "+createRoom);
    }

    public void SetSearchRoomEntered(string searchRoom)
    {
        searchRoomEntered = searchRoom;
        Debug.Log("CreateRoomMenu: SetSearchRoomEntered "+searchRoom);
    }

    public void Configure(MenuMediator mediator)
    {
        this.mediator = mediator;
        Debug.Log("CreateRoomMenu: Configure");
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Debug.Log("CreateRoomMenu: Show");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Debug.Log("CreateRoomMenu: Hide");
    }
}
