using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// using DG.Tweening;

public class CreateAccountMenu : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button createButton;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_InputField passwordInputRepeat;
    private MenuMediator mediator;

    private string newUserName = "";
    private string newUserPassword = "";
    private string newUserPasswordRepeat = "";

    private void Awake()
    {
        backButton.onClick.AddListener(() => mediator.OpenLoginMenu());
        createButton.onClick.AddListener(() => getNewAccount());
        // backButton.onClick.AddListener(new UnityAction(mediator.BackToMainMenu()));
    }

    private void getNewAccount(){
        string newUserName = nameInput.text;
        string newUserPassword = passwordInput.text;
        string newUserPasswordRepeat = passwordInputRepeat.text;
        Debug.Log("Create Account Menu: getNewAccount");
        if(newUserPassword == newUserPasswordRepeat){
            mediator.CreateAccount(newUserName, newUserPassword);
        }else{
            Debug.Log("Create Account Menu: getNewAccount !! passwords not equals");
        }
    }

    public void SetNewUserName(string name){
        newUserName =name;
        Debug.Log("CreateAccountMenu: SetNewUserName "+name);
    }

    public void SetNewUserPassword(string password){
        newUserPassword =password;
        Debug.Log("CreateAccountMenu: SetNewUserPassword "+password);
    }

    public void SetNewUserPasswordRepeat(string password){
        newUserPasswordRepeat =password;
        Debug.Log("CreateAccountMenu: SetNewUserPasswordRepeat "+password);
    }

    public void Configure(MenuMediator mediator){
        this.mediator = mediator;
        Debug.Log("CreateAccountMenu: Configure");
    }

    public void Show()
    {
        gameObject.SetActive(true);
        // canvasGroup.DOFade(1.0f, 0.5f);
        Debug.Log("CreateAccountMenu: Show");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        // canvasGroup.DOFade(0.0f, 0.5f);
        Debug.Log("CreateAccountMenu: Hide");
    }
}
