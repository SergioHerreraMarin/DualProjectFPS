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
    private MenuMediator mediator;

    private void Awake()
    {
        startGameButton.onClick.AddListener(() => mediator.OpenCreateRoomMenu());
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
        gameObject.SetActive(true);
        Debug.Log("Main Menu: Show");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Debug.Log("Main Menu: Hide");
    }

}
