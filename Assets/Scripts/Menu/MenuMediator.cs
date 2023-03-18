using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using DG.Tweening;

public class MenuMediator : MonoBehaviour
{
    /* De momento he pensado 5 menus para nuestra interfaz, puede que alguno no sea necesario
    pero lo he planteado asi por que creo que cubre distintas posibilidades 

    el principal, el de login, el de crear usuario, 
    el de opciones para configurar parametros 
    y el de perfil, donde el usuario podria ver info de su perfil o cambiar la cuenta/perfil con la que juega*/
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private SettingsMenu settingsMenu;
    [SerializeField] private ProfileMenu profileMenu;
    [SerializeField] private LoginMenu loginMenu;
    [SerializeField] private CreateAccountMenu createAccountMenu;

    /* Aparte de tener los menus, tendra también un objeto de la clase usuario
    que se instancia o bien creando usuario o haciendo login,
    este es el usuario que jugara, o bien se obtiene con un login o se crea */
    private UserProfile currentUser= null;

    /* Metodo de inicio, primero se vera el menu de Login */

    private void Awake(){
        mainMenu.Configure(this);
        settingsMenu.Configure(this);
        profileMenu.Configure(this);
        loginMenu.Configure(this);
        createAccountMenu.Configure(this);

        loginMenu.Show();
        mainMenu.Hide();
        settingsMenu.Hide();
        profileMenu.Hide();
        createAccountMenu.Hide();
        Debug.Log("Menu Mediator: Awake");
    }

    /* Metodos para movernos entre menus */

    public void BackToMainMenu(){
        mainMenu.Show();

        settingsMenu.Hide();
        profileMenu.Hide();
        loginMenu.Hide();
        createAccountMenu.Hide();

        Debug.Log("Menu Mediator: Back to Main Menu");
    }

    public void StartGame(){
        if(currentUser != null)
        {
            mainMenu.Hide();
            settingsMenu.Hide();
            profileMenu.Hide();
            loginMenu.Hide();
            createAccountMenu.Hide();

            Debug.Log("Menu Mediator: Start Game");
            SceneManager.LoadScene("TestPlayerScene");
        }else{
            Debug.Log("Menu Mediator: Start Game !! No user logged in");
        }
    }

    public void QuitGame(){
        mainMenu.Hide();
        settingsMenu.Hide();
        profileMenu.Hide();
        loginMenu.Hide();
        createAccountMenu.Hide();

        Debug.Log("Menu Mediator: Quit Game");
        Application.Quit();
    }

    public void OpenProfileMenu(){
        if(currentUser != null)
        {
            profileMenu.Show();

            mainMenu.Hide();
            settingsMenu.Hide();
            loginMenu.Hide();
            createAccountMenu.Hide();

            Debug.Log("Menu Mediator: Open Profile Menu");
        }else{
            Debug.Log("Menu Mediator: Open Profile Menu !! No user logged in");
        }

    }

    public void OpenSettings(){
        profileMenu.Hide();

        mainMenu.Hide();
        settingsMenu.Show();
        loginMenu.Hide();
        createAccountMenu.Hide();

        Debug.Log("Menu Mediator: Open Settings");
    }

    public void OpenLoginMenu(){
        loginMenu.Show();

        mainMenu.Hide();
        settingsMenu.Hide();
        profileMenu.Hide();
        createAccountMenu.Hide();

        Debug.Log("Menu Mediator: Open Login Menu");
    }

    public void OpenCreateAccountMenu(){
        profileMenu.Hide();

        mainMenu.Hide();
        settingsMenu.Hide();
        loginMenu.Hide();
        createAccountMenu.Show();

        Debug.Log("Menu Mediator: Open Create Account Menu");
    }

    /* Metodos relacionados con la gestion de usuarios, falta la parte de conexión de BBDD */

    /* Si ninguno de los campos esta vacio, crea un usuario en la BBDD */
    public void CreateAccount(string username, string password){
        if(username == "" || password == ""){
            Debug.Log("Menu Mediator: Create Account: Username or Password is empty");
            return;
        }else{
            Debug.Log("Menu Mediator: Create Account: Username: " + username + " Password: " + password);
            
            /* Primero comprueba si existe dicho usuario (username como Primary Key por ejemplo) 
            si no existe, se crea y se establece como el usuario atual,
            que se guarda en la BBDD*/

            currentUser = new UserProfile(username, password);
        }
    }

    /*Con el nombre de usuario y la contraseña que le pasamos, comprueba si
    existe en la base de datos */
    public void CheckLogin(string username, string password){
        bool userExists = true;

        /* Falta codigo de comprovación en BBDD si existe, 
        si existe, lo establece como usuario, va al menu principal y
        habilita un boton en el menu de login que estaba inhabilitado */

        if(userExists){
            currentUser = new UserProfile(username, password);
            loginMenu.EnableBackButton();
            BackToMainMenu();
        }
    }

    /* Funcion de testing para no tener que entrar credenciales */
    public void FastLogin(){
        currentUser = new UserProfile("test", "test");
    }
}
