using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using DG.Tweening;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Button backButton;
    private MenuMediator mediator;

    private void Awake()
    {
        backButton.onClick.AddListener(() => mediator.BackToMainMenu());
    }

    public void Configure(MenuMediator mediator)
    {
        this.mediator = mediator;
        Debug.Log("Settings Menu: Configure");
    }

    public void Show()
    {
        gameObject.SetActive(true);
        // canvasGroup.DOFade(1.0f, 0.5f);
        Debug.Log("Settings Menu: Show");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        // canvasGroup.DOFade(0.0f, 0.5f);
        Debug.Log("Settings Menu: Hide");
    }

}
