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
    [SerializeField] private Dropdown dropdownRanking;
    private MenuMediator mediator;
    private List<UserProfile> usersRanking;

    private void Awake()
    {
        backButton.onClick.AddListener(() => mediator.BackToMainMenu());
        changeAccountButton.onClick.AddListener(() => mediator.OpenLoginMenu(true));
        modifyAccountButton.onClick.AddListener(() => mediator.OpenModifyAccountMenu(true));
        dropdownRanking.onValueChanged.AddListener(delegate {DropdownItemSelected(dropdownRanking);});

        // backButton.onClick.AddListener(new UnityAction(mediator.BackToMainMenu()));
    }

    public void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        switch (index)
        {
        case 0:
            Debug.Log("Matches Won selected");
            retrieveRankingList("matchesWon");
            break;
        case 1:
            Debug.Log("Matches Lost selectes");
            retrieveRankingList("matchesLost");
            break;
        case 2:
            Debug.Log("Enemies Killes selected");
            retrieveRankingList("enemiesKilled");
            break;
        case 3:
            Debug.Log("Deaths selected");
            retrieveRankingList("deaths");
            break;
        default:
            break;
        }
    }

    /* Pedira a la BBDD el ranking de usuarios */
    public void retrieveRankingList(string parameter){
        Debug.Log("Ranking Menu: retrieveRankingList");
        usersRanking= mediator.RetrieveRanking(parameter);
        foreach(UserProfile u in usersRanking){
            Debug.Log("User retrieve:"+ u.ToString());
        }
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
