using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
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
    [SerializeField] private CreateRoomMenu createRoomMenu;
    [SerializeField] private ModifyAccountMenu modifyAccountMenu;
    [SerializeField] private PanelMessage panelMessage;
    [SerializeField] private PanelConfirmation panelConfirmation;
    [SerializeField] private AudioSource music1;
    [SerializeField] private AudioSource music2;

    private DatabaseMediator databaseMediator;

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
        createRoomMenu.Configure(this);
        modifyAccountMenu.Configure(this);
        panelMessage.Configure(this);
        panelConfirmation.Configure(this);   

        playMusic1();

        hideAll();
        loginMenu.Show();
        Debug.Log("Menu Mediator: Awake");

    }

    public void ConfigureDbMediator(DatabaseMediator databaseMediator){
        this.databaseMediator = databaseMediator;
        Debug.Log("Medu Mediator: ConfigureDbMediator");
    }

    /* Metodos para movernos entre menus */

    public void BackToMainMenu(){
        hideAll();
        mainMenu.Show();
        PhotonNetwork.Disconnect(); //Desconectar del server al salir del menú. 
        Debug.Log("Menu Mediator: Back to Main Menu");
    }

    public void showMessagePanel(string message){
        disableAllButtons();

        panelMessage.Show(message);
        Debug.Log("Menu Mediator: Show Message Panel");
    }

    public void hideMessagePanel(){
        enableAllButtons();

        panelMessage.Hide();
        Debug.Log("Menu Mediator: Hide Message Panel");
    }

    public void showConfirmationPanel(string message){
        disableAllButtons();

        panelConfirmation.Show(message);
        Debug.Log("Menu Mediator: Show Message Panel");
    }

    public void hideConfirmationPanel(){
        enableAllButtons();

        panelConfirmation.Hide();
        Debug.Log("Menu Mediator: Hide Message Panel");
    }

    public void StartGame(){
        if(currentUser != null)
        {
            hideAll();

            Debug.Log("Menu Mediator: Start Game");
            SceneManager.LoadScene("TestPlayerScene");
        }else{
            Debug.Log("Menu Mediator: Start Game !! No user logged in");
        }
    }

    public void QuitGame(){
        this.hideAll();

        Debug.Log("Menu Mediator: Quit Game");
        Application.Quit();
    }

    public void OpenProfileMenu(){
        if(currentUser != null)
        {
            this.hideAll();
            profileMenu.Show();

            Debug.Log("Menu Mediator: Open Profile Menu");
        }else{
            Debug.Log("Menu Mediator: Open Profile Menu !! No user logged in");
        }
    }

    public void OpenSettings(){
        this.hideAll();
        settingsMenu.Show();

        Debug.Log("Menu Mediator: Open Settings");
    }

    public void OpenLoginMenu(){
        this.hideAll();
        loginMenu.Show();

        Debug.Log("Menu Mediator: Open Login Menu");
    }

    public void OpenCreateAccountMenu(){
        this.hideAll();
        createAccountMenu.Show();

        Debug.Log("Menu Mediator: Open Create Account Menu");
    }

    public void OpenModifyAccountMenu(){
        this.hideAll();
        modifyAccountMenu.Show();

        Debug.Log("Menu Mediator: Open Modify Account Menu");
    }

    public void OpenCreateRoomMenu(){
        this.hideAll();
        createRoomMenu.Show();
        playMusic2();
        Debug.Log("Menu Mediator: Open Create Room Menu");
    }

    private void playMusic1(){
        if(music1.isPlaying == false){
            music1.Play();
            music2.Stop();
            Debug.Log("Menu Mediator: Play Music 1");
        }
    }

    private void playMusic2(){
        if (music2.isPlaying == false)
        {
            music2.Play();
            music1.Stop();
            Debug.Log("Menu Mediator: Play Music 2");
        }
    }

    public void hideAll(){
        playMusic1();
        mainMenu.Hide();
        settingsMenu.Hide();
        profileMenu.Hide();
        loginMenu.Hide();
        createAccountMenu.Hide();
        createRoomMenu.Hide();
        modifyAccountMenu.Hide();
    }

    public void enableAllButtons(){
        mainMenu.enableButtons();
        settingsMenu.enableButtons();
        profileMenu.enableButtons();
        loginMenu.enableButtons();
        createAccountMenu.enableButtons();
        createRoomMenu.enableButtons();
        modifyAccountMenu.enableButtons();
    }

    public void disableAllButtons(){
        mainMenu.disableButtons();
        settingsMenu.disableButtons();
        profileMenu.disableButtons();
        loginMenu.disableButtons();
        createAccountMenu.disableButtons();
        createRoomMenu.disableButtons();
        modifyAccountMenu.disableButtons();
    }

    /* Metodos relacionados con la gestion de usuarios, falta la parte de conexión de BBDD */

    /* Si ninguno de los campos esta vacio, crea un usuario en la BBDD */
    public void CreateAccount(string username, string password){
            Debug.Log("MenuMediator: CreateAccount: Username: " + username + " Password: " + password);
            
            /* Primero comprueba si existe dicho usuario (username como Primary Key por ejemplo) 
            si no existe, se crea en la BBDD y para establecerlo, lo obtiene de la BBDD
            */

            bool exists = databaseMediator.checkUserExists(username);
            if(exists == false){
                databaseMediator.insertNewUser(username, password, 0);
                startUser(databaseMediator.retrieveUserByName(username));
                showMessagePanel("User with name "+username+" created successfully\n and setted as current user");
            }else{
                showMessagePanel("User with name "+username+" already exists in the database");
            }
    }

/* Funcion para modificar nombre y/o contraseña, le pasamos el nuevo nombre y nueva contraseña
si alguno de estos es nulo, se usara el valor actual (y en la practica no se actualizara, seguira igual) */
    public void ModifyAccount(string newUsername, string newPassword, string oldName){
        Debug.Log("MenuMediator: ModifyAccount: New Username: " + newUsername + " New Password: " + newPassword);
        bool nameExists = false;
        if(newUsername == ""){
            newUsername = currentUser.getUserName();
        }
        if(newPassword == ""){
            newPassword = currentUser.getUserPassword();
        }
        nameExists = databaseMediator.checkUserExists(newUsername);
        if (nameExists == false || newUsername == oldName){
            databaseMediator.updateUser(currentUser, newUsername, newPassword, currentUser.getUserScore());
            startUser(databaseMediator.retrieveUserByName(newUsername));
            showMessagePanel("User with name "+oldName+" changed to "+newUsername+" successfully");
        }else{
            showMessagePanel("The name "+newUsername+" is already registered\n in the database and can't be used");
        }
    }

    public void DeleteAccount(){
        Debug.Log("MenuMediator: DeleteAccount");
        if(databaseMediator.deleteUser(currentUser)){
            currentUser = null;
            hideAll();
            loginMenu.Show();
            loginMenu.DisableBackButton();
            showMessagePanel("User deleted successfully");
        }else{
            showMessagePanel("Error deleting the user");
        }
        

    }

    /*Con el nombre de usuario y la contraseña que le pasamos, comprueba si
    existe en la base de datos */
    public void CheckLogin(string username, string password){
        Debug.Log("Menu Mediator: Check Login: Username: " + username + " Password: " + password);

        if(username == "" || password == ""){
            Debug.Log("Menu Mediator: Check Login: Username or Password is empty");
            return;
        }else{
            /* Si existe y contraseña OK, lo establecera como usuario, va al menu principal y
            habilita un boton en el menu de login que estaba inhabilitado */

           /*  databaseMediator.testDatabase();
            databaseMediator.sampleQuery(); */

            if(databaseMediator.checkUserExists(username) == true){
                if(databaseMediator.checkUserPassword(username, password) == true){
                    startUser(databaseMediator.retrieveUserByName(username));
                    showMessagePanel("Welcome "+username+"!");
                }else{
                    Debug.Log("Menu Mediator: Check Login: Password incorrect for this user");
                    showMessagePanel("Password incorrect for this user");
                }
            }else{
                Debug.Log("Menu Mediator: Check Login: User not found");
                showMessagePanel("User not found, you can create a user with this name");
            }
        }
    }

/* Funcion con la que establecemos que usuario va a ser el currentUser, servira tanto para 
hacer loguin, como para cuando creamos un usuario nuevo. Si podemos establecer un nuevo usuario, iremos al menu principal
desbloquearemos el boton dle menu login para ir al menu principal y en el menu de perfil estableceremos los datos de ese nuevo usuario */
    private void startUser(UserProfile userLogged){
        if(userLogged != null){
            currentUser = userLogged;
            loginMenu.EnableBackButton();
            BackToMainMenu();
            profileMenu.setNameValue(currentUser.getUserName());
        }else{
            Debug.Log("Menu Mediator: Start User: User is null");
            showMessagePanel("Something went wrong, user could not be loaded correctly");
        }
    }

    public UserProfile getCurrentUser(){
        return currentUser;
    }

    /* En este metodo crearia una sala de juego con el nombre que ha pasado el jugador */
    public void NewRoom(string roomRame){
        Debug.Log("Menu Mediator: New Room");
    }

    /* Con este metodo, a partir del nombre de sala, comprueba si existe y de ser asi se uniria */
    public void JoinRoom(string roomName){
        Debug.Log("Menu Mediator: Join Room");
    }
}
