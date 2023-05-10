using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* Menu para ver datos del perfil i estadisticas,
Aqui tenemos la posibilidad de canviar nuestra cuenta volviendo a Login */

public class ProfileMenu : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button changeAccountButton;
    [SerializeField] private Button modifyAccountButton;
    [SerializeField] private TextMeshProUGUI nameValue;
    [SerializeField] private TextMeshProUGUI matchesWonValue;
    [SerializeField] private TextMeshProUGUI matchesLostValue;
    [SerializeField] private TextMeshProUGUI enemiesKilledValue;
    [SerializeField] private TextMeshProUGUI deathsValue;
    private MenuMediator mediator;

    private void Awake()
    {
        backButton.onClick.AddListener(() => mediator.BackToMainMenu());
        changeAccountButton.onClick.AddListener(() => mediator.OpenLoginMenu());
        modifyAccountButton.onClick.AddListener(() => mediator.OpenModifyAccountMenu(false));

        // backButton.onClick.AddListener(new UnityAction(mediator.BackToMainMenu()));
    }

    public void Configure(MenuMediator mediator){
        this.mediator = mediator;
        Debug.Log("Profile Menu: Configure");
    }

    public void Show()
    {
        // Debug.Log("Profile Menu: Show");
        gameObject.SetActive(true);
        // canvasGroup.DOFade(1.0f, 0.5f);
    }

    public void Hide()
    {
        // Debug.Log("Profile Menu: Hide");
        gameObject.SetActive(false);
        // canvasGroup.DOFade(0.0f, 0.5f);
    }

    public void setNameValue(string userName){
        nameValue.text = userName;
        Debug.Log("Profile Menu: setNameValue "+userName);
    }

    public void setMatchesWon(int matchesWon){
        matchesWonValue.text = matchesWon.ToString();
        Debug.Log("Profile Menu: setMatchesWon "+matchesWon);
    }

    public void setMatchesLost(int matchesLost){
        matchesLostValue.text = matchesLost.ToString();
        Debug.Log("Profile Menu: setMatchesLost "+matchesLost);
    }

    public void setEnemiesKilled(int enemiesKilled){
        enemiesKilledValue.text = enemiesKilled.ToString();
        Debug.Log("Profile Menu: setEnemiesKilled "+enemiesKilled);
    }

    public void setDeaths(int deaths){
        deathsValue.text = deaths.ToString();
        Debug.Log("Profile Menu: setDeaths "+deaths);
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
