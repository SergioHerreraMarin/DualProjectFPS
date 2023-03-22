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
    [SerializeField] private TextMeshProUGUI nameValue;
    [SerializeField] private CanvasGroup canvasGroup;
    private MenuMediator mediator;

    private void Awake()
    {
        backButton.onClick.AddListener(() => mediator.BackToMainMenu());
        changeAccountButton.onClick.AddListener(() => mediator.OpenLoginMenu());

        // backButton.onClick.AddListener(new UnityAction(mediator.BackToMainMenu()));
    }

    public void Configure(MenuMediator mediator){
        this.mediator = mediator;
        Debug.Log("Profile Menu: Configure");
    }

    public void Show()
    {
        gameObject.SetActive(true);
        // canvasGroup.DOFade(1.0f, 0.5f);
        Debug.Log("Profile Menu: Show");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        // canvasGroup.DOFade(0.0f, 0.5f);
        Debug.Log("Profile Menu: Hide");
    }

    public void setNameValue(string userName){
        nameValue.text = userName;
        Debug.Log("Profile Menu: setNameValue "+userName);
    }
}
