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
    [SerializeField] private CanvasGroup canvasGroup;
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
    }

/* Si lograra asignar los inputField, podria usar este metodo que lea lo que el usuario
ha entrado al pulsar el boton y lo mande al mediator, el ual si esta todo ok permitira
ingresar al menu principal y a todas las opciones, entonces qedaria 
loginButton.onClick.AddListener(() => Login());*/
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
