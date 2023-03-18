using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using DG.Tweening;

/* Menu principal */

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button profileButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private CanvasGroup canvasGroup;
    private MenuMediator mediator;

    private void Awake()
    {
        startGameButton.onClick.AddListener(() => mediator.StartGame());
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
        // canvasGroup.DOFade(1.0f, 0.5f);
        Debug.Log("Main Menu: Show");
    }

    public void Hide()
    {
        // canvasGroup.DOFade(0.0f, 0.5f);
        Debug.Log("Main Menu: Hide");
    }

}
