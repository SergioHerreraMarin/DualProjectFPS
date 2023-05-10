using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankingMenu : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button changeAccountButton;
    [SerializeField] private Button modifyAccountButton;
    private MenuMediator mediator;

    private void Awake()
    {
        backButton.onClick.AddListener(() => mediator.BackToMainMenu());
        changeAccountButton.onClick.AddListener(() => mediator.OpenLoginMenu(true));
        modifyAccountButton.onClick.AddListener(() => mediator.OpenModifyAccountMenu(true));

        // backButton.onClick.AddListener(new UnityAction(mediator.BackToMainMenu()));
    }

    public void Configure(MenuMediator mediator){
        this.mediator = mediator;
        Debug.Log("Ranking Menu: Configure");
    }

    public void Show()
    {
        // Debug.Log("Ranking Menu: Show");
        gameObject.SetActive(true);
        // canvasGroup.DOFade(1.0f, 0.5f);
    }

    public void Hide()
    {
        // Debug.Log("Ranking Menu: Hide");
        gameObject.SetActive(false);
        // canvasGroup.DOFade(0.0f, 0.5f);
    }

    public void enableButtons(){
        // Debug.Log("Profile Menu: enableButtons");
        backButton.interactable = true;
        changeAccountButton.interactable = true;
        modifyAccountButton.interactable = true;
    }

    public void disableButtons(){
        // Debug.Log("Profile Menu: disableButtons");
        backButton.interactable = false;
        changeAccountButton.interactable = false;
        modifyAccountButton.interactable = false;
    }
}
