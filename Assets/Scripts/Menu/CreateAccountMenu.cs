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
        Debug.Log("Create Account Menu: getNewAccount");
        string newUserName = nameInput.text;
        string newUserPassword = passwordInput.text;
        string newUserPasswordRepeat = passwordInputRepeat.text;
        
        if(newUserName == "" || newUserPassword == "" || newUserPasswordRepeat == ""){
            mediator.ShowMessagePanel("You must fulfill all the fields");
            return;
        }else if(newUserPassword == newUserPasswordRepeat){
            mediator.CreateAccount(newUserName, newUserPassword);
        }else{
            mediator.ShowMessagePanel("Passwords do not match");
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
        Debug.Log("CreateAccountMenu: Configure");
        this.mediator = mediator;
    }

    public void Show()
    {
        // Debug.Log("CreateAccountMenu: Show");
        gameObject.SetActive(true);
        // canvasGroup.DOFade(1.0f, 0.5f);
    }

    public void Hide()
    {
        // Debug.Log("CreateAccountMenu: Hide");
        gameObject.SetActive(false);
        nameInput.text = "";
        passwordInput.text = "";
        passwordInputRepeat.text = "";
        // canvasGroup.DOFade(0.0f, 0.5f);
    }

    public void enableButtons(){
        // Debug.Log("CreateAccountMenu: Enable Buttons");
        backButton.interactable = true;
        createButton.interactable = true;
    }

    public void disableButtons(){
        // Debug.Log("CreateAccountMenu: Disable Buttons");
        backButton.interactable = false;
        createButton.interactable = false;
    }
}
