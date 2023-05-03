using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using DG.Tweening;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundsSlider;
    private MenuMediator mediator;

    private void Awake()
    {
        backButton.onClick.AddListener(() => mediator.BackToMainMenu());
        musicSlider.onValueChanged.AddListener((value) => mediator.SetMusicVolume(value));
        soundsSlider.onValueChanged.AddListener((value) => mediator.SetSoundsVolume(value));
    }

    public void Configure(MenuMediator mediator)
    {
        this.mediator = mediator;
        Debug.Log("Settings Menu: Configure");
    }

    public void Show()
    {
        // Debug.Log("Settings Menu: Show");
        gameObject.SetActive(true);
        // canvasGroup.DOFade(1.0f, 0.5f);
    }

    public void Hide()
    {
        // Debug.Log("Settings Menu: Hide");
        gameObject.SetActive(false);
        // canvasGroup.DOFade(0.0f, 0.5f);
    }

    public void enableButtons(){
        // Debug.Log("Settings Menu: enableButtons");
        backButton.interactable = true;
    }

    public void disableButtons(){
        // Debug.Log("Settings Menu: disableButtons");
        backButton.interactable = false;
    }

}
