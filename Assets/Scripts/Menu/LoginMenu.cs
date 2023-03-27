using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/* Menu para loguearse, sera el primero que se muestre, si el login va bien
nos lleva al menu principal, otra opcion es que vayamos al menu de crearnos un usuario */

public class LoginMenu : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button createAccountButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField passwordInput;
    private MenuMediator mediator;

    private string nameEntered ="";
    private string passwordEntered="";

    private void Awake()
    {
        backButton.interactable = false;
        backButton.onClick.AddListener(() => mediator.OpenProfileMenu());
        loginButton.onClick.AddListener(() => Login());
        createAccountButton.onClick.AddListener(() => mediator.OpenCreateAccountMenu());
        quitButton.onClick.AddListener(() => mediator.QuitGame());
    }

    private void Login(){
        string nameEntered = nameInput.text;
        string passwordEntered = passwordInput.text;
        mediator.CheckLogin(nameEntered, passwordEntered);
        Debug.Log("LoginMenu: Login nameEntered "+nameEntered+" passwordEntered "+passwordEntered);
    }

    public void EnableBackButton()
    {
        backButton.interactable = true;
    }

    public void SetNameEntered(string name)
    {
        nameEntered = name;
        Debug.Log("LoginMenu: SetNameEntered "+name);
    }

    public void SetPasswordEntered(string password)
    {
        passwordEntered = password;
        Debug.Log("LoginMenu: SetPasswordEntered "+password);
    }

    public void Configure(MenuMediator mediator)
    {
        this.mediator = mediator;
        Debug.Log("LoginMenu: Configure");
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Debug.Log("LoginMenu: Show");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Debug.Log("LoginMenu: Hide");
    }
}
