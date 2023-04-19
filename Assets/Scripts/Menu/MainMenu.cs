using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Menu principal */

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button profileButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private ConnectToServer connectToServer;
    private MenuMediator mediator;

    private void Awake()
    {
        startGameButton.onClick.AddListener(() => ConnectToPhotonServer());
        settingsButton.onClick.AddListener(() => mediator.OpenSettings());
        profileButton.onClick.AddListener(() => mediator.OpenProfileMenu());
        quitButton.onClick.AddListener(() => mediator.QuitGame());
    }

    public void Configure(MenuMediator mediator)
    {
        this.mediator = mediator;
        Debug.Log("Main Menu: Configure");
    }

    public void Show()
    {
        Debug.Log("Main Menu: Show");
        enableButtons();
        gameObject.SetActive(true);
        
    }

    public void Hide()
    {
        Debug.Log("Main Menu: Hide");
        gameObject.SetActive(false);
    }

    public void enableButtons(){
        // Debug.Log("Main Menu: Enable Buttons");
        startGameButton.interactable = true;
        settingsButton.interactable = true;
        profileButton.interactable = true;
        quitButton.interactable = true;
    }

    public void disableButtons(){
        // Debug.Log("Main Menu: Disable Buttons");
        startGameButton.interactable = false;
        settingsButton.interactable = false;
        profileButton.interactable = false;
        quitButton.interactable = false;
    }

    private void ConnectToPhotonServer()
    {
        connectToServer.Connect();
    }

}
