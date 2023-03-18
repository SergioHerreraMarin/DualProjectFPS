using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using DG.Tweening;

/* Menu para ver datos del perfil i estadisticas,
Aqui tenemos la posibilidad de canviar nuestra cuenta volviendo a Login */

public class ProfileMenu : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button changeAccountButton;
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
        // canvasGroup.DOFade(1.0f, 0.5f);
        Debug.Log("Profile Menu: Show");
    }

    public void Hide()
    {
        // canvasGroup.DOFade(0.0f, 0.5f);
        Debug.Log("Profile Menu: Hide");
    }
}
