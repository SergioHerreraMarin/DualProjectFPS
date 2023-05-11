using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Threading;

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
    [SerializeField] private RankingMenu rankingMenu;
    [SerializeField] private PanelMessage panelMessage;
    [SerializeField] private PanelConfirmation panelConfirmation;

    [SerializeField] private AudioSource music1;
    [SerializeField] private AudioSource music2;
    [SerializeField] private AudioSource backButtonSound;
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource inputSound;
    [SerializeField] private AudioSource exitSound;

    private DatabaseMediator databaseMediator;

    /* Aparte de tener los menus, tendra también un objeto de la clase usuario
    que se instancia o bien creando usuario o haciendo login,
    este es el usuario que jugara, o bien se obtiene con un login o se crea */
    private UserProfile currentUser= null;

    private bool confirmationAccepted = false;

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
        rankingMenu.Configure(this);

        PlayMusic1();

        hideAll();
        loginMenu.Show();
        Debug.Log("Menu Mediator: Awake");

    }

    /* Metodos para movernos entre menus */

    public void BackToMainMenu(){
        hideAll();
        mainMenu.Show();
        PhotonNetwork.Disconnect(); //Desconectar del server al salir del menú. 
        Debug.Log("Menu Mediator: Back to Main Menu");
    }

    public void ShowMessagePanel(string message){
        disableAllButtons();

        panelMessage.Show(message);
        Debug.Log("Menu Mediator: Show Message Panel");
    }

    public void hideMessagePanel(){
        enableAllButtons();

        panelMessage.Hide();
        Debug.Log("Menu Mediator: Hide Message Panel");
    }

    //Codigo relacionado con el panel de confirmacion, este requiere esperar que el usuario acepte o rechaze

    public void showConfirmationPanel(bool inputActive, string message){
        Debug.Log("Menu Mediator: Show Confirmation Panel");
        disableAllButtons();
        panelConfirmation.Show(inputActive, message);
    }

    public void hideConfirmationPanel(){
        enableAllButtons();

        panelConfirmation.Hide();
        Debug.Log("Menu Mediator: Hide Message Panel");
    }

    public void setConfirmationAccepted(bool accepted){
        Debug.Log("Menu Mediator: Set Confirmation Accepted to " + accepted.ToString() + "");
        this.confirmationAccepted = accepted;
    }

    public bool getConfirmationAccepted(){
        return this.confirmationAccepted;
    }

    //Fin mtodos relacionados con el panel de confirmacion

    public void ConfigureDbMediator(DatabaseMediator databaseMediator){
        this.databaseMediator = databaseMediator;
        Debug.Log("Medu Mediator: ConfigureDbMediator");
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

        databaseMediator.SetUserLoggedOut(currentUser.GetUserName());
        Debug.Log("Menu Mediator: Quit Game");
        music1.Stop();
        music2.Stop();
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

    public void OpenLoginMenu(bool fromRanking){
        this.hideAll();
        loginMenu.Show(fromRanking);

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

    public void OpenModifyAccountMenu(bool fromRanking){
        this.hideAll();
        modifyAccountMenu.Show(fromRanking);

        Debug.Log("Menu Mediator: Open Modify Account Menu");
    }

    public void OpenCreateRoomMenu(){
        this.hideAll();
        createRoomMenu.Show();
        PlayMusic2();
        Debug.Log("Menu Mediator: Open Create Room Menu");
    }

    public void OpenRankingMenu(){
        this.hideAll();
        rankingMenu.Show();

        Debug.Log("Menu Mediator: Open Ranking Menu");
    }

    private void PlayMusic1(){
        if(music1.isPlaying == false){
            music1.Play();
            music2.Stop();
            Debug.Log("Menu Mediator: Play Music 1");
        }
    }

    private void PlayMusic2(){
        if (music2.isPlaying == false)
        {
            music2.Play();
            music1.Stop();
            Debug.Log("Menu Mediator: Play Music 2");
        }
    }

    public void SetMusicVolume(float volume){
        music1.volume = volume;
        music2.volume = volume;
        Debug.Log("Menu Mediator: Set Music Volume");
    }

    public void SetSoundsVolume(float volume){
        backButtonSound.volume = volume;
        buttonSound.volume = volume;
        inputSound.volume = volume;
        exitSound.volume = volume;
        Debug.Log("Menu Mediator: Set Sounds Volume");
    }

    public void hideAll(){
        PlayMusic1();
        mainMenu.Hide();
        settingsMenu.Hide();
        profileMenu.Hide();
        loginMenu.Hide();
        createAccountMenu.Hide();
        createRoomMenu.Hide();
        modifyAccountMenu.Hide();
        rankingMenu.Hide();
    }

    public void enableAllButtons(){
        mainMenu.enableButtons();
        settingsMenu.enableButtons();
        profileMenu.enableButtons();
        loginMenu.enableButtons();
        createAccountMenu.enableButtons();
        createRoomMenu.enableButtons();
        modifyAccountMenu.enableButtons();
        rankingMenu.enableButtons();
    }

    public void disableAllButtons(){
        mainMenu.disableButtons();
        settingsMenu.disableButtons();
        profileMenu.disableButtons();
        loginMenu.disableButtons();
        createAccountMenu.disableButtons();
        createRoomMenu.disableButtons();
        modifyAccountMenu.disableButtons();
        rankingMenu.disableButtons();
    }

    /* Metodos relacionados con la gestion de usuarios, falta la parte de conexión de BBDD */

    /* Si ninguno de los campos esta vacio, crea un usuario en la BBDD */
    public void CreateAccount(string username, string password){
            Debug.Log("MenuMediator: CreateAccount: Username: " + username + " Password: " + password);
            
            /* Primero comprueba si existe dicho usuario (username como Primary Key por ejemplo) 
            si no existe, se crea en la BBDD y para establecerlo, lo obtiene de la BBDD
            */

            bool exists = databaseMediator.CheckUserExists(username);
            if(exists == false){
                databaseMediator.insertNewUser(username, password, false, 0, 0, 0, 0);
                startUser(databaseMediator.retrieveUserByName(false, username));
                ShowMessagePanel("User with name "+username+" created successfully\n and setted as current user");
            }else{
                ShowMessagePanel("User with name "+username+" already exists in the database");
            }
    }

/* Funcion para modificar nombre y/o contraseña, le pasamos el nuevo nombre y nueva contraseña
si alguno de estos es nulo, se usara el valor actual (y en la practica no se actualizara, seguira igual) */
    public void ModifyAccount(string newUsername, string newPassword, string oldName){
        Debug.Log("MenuMediator: ModifyAccount: New Username: " + newUsername + " New Password: " + newPassword);
        bool nameExists = false;
        if(newUsername == ""){
            newUsername = currentUser.GetUserName();
        }
        if(newPassword == ""){
            newPassword = currentUser.GetUserPassword();
        }
        nameExists = databaseMediator.CheckUserExists(newUsername);
        if (nameExists == false || newUsername == oldName){
            databaseMediator.updateUser(currentUser, newUsername, newPassword, currentUser.GetMatchesWon(), currentUser.GetMatchesLost(), currentUser.GetEnemiesKilled(), currentUser.GetDeaths());
            startUser(databaseMediator.retrieveUserByName(false, newUsername));
            ShowMessagePanel("User with name "+oldName+" modified successfully");
        }else{
            ShowMessagePanel("The name "+newUsername+" is already registered\n in the database and can't be used");
        }
    }

/* Emplea esta funcion asincrona porque eliminar un jugador requiere que se abra un cuadro que espera confirmacion
del usuario, este puede decir OK, borralo, o echarse para atras*/
    public void DeleteAccount(){
        Debug.Log("MenuMediator: DeleteAccount");
        showConfirmationPanel(false, "Do you want to delete this account?");
        StartCoroutine(waitToDeleteAccount());
    }

/* con el while, espera a que desde el panel de cofnirmación de decida OK o REJECT,
si la confirmación es OK, entonces tenemos que llevar a cabo la accion de borrar el usuario */
    public IEnumerator waitToDeleteAccount(){
        Debug.Log("MenuMediator: waitToDeleteAccount");
        while(panelConfirmation.isSubmitted() == false){
            yield return null;
        }
        Debug.Log("confirmationAccepted: " + confirmationAccepted);
        if(confirmationAccepted == true){
            if(databaseMediator.deleteUser(currentUser)){
                currentUser = null;
                hideAll();
                loginMenu.Show();
                loginMenu.DisableBackButton();
                ShowMessagePanel("User deleted successfully");
            }else{
                ShowMessagePanel("Error deleting the user");
            }
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

            if(databaseMediator.CheckUserExists(username) == true){
                if(databaseMediator.CheckUserPassword(username, password) == true){
                    if(currentUser != null){
                        /*Si hace esto cuando ya se ha logeado previamente, es decir cambio de usuario,
                         vamos a indicar que el antiguo usuario ya no esta logeado */
                        databaseMediator.SetUserLoggedOut(currentUser.GetUserName());
                    }
                    if(startUser(databaseMediator.retrieveUserByName(true, username))){
                        ShowMessagePanel("Welcome "+username+"!");
                    }
                }else{
                    Debug.Log("Menu Mediator: Check Login: Password incorrect for this user");
                    ShowMessagePanel("Password incorrect for this user");
                }
            }else{
                Debug.Log("Menu Mediator: Check Login: User not found");
                ShowMessagePanel("User not found, you can create a user with this name");
            }
        }
    }

/* Funcion con la que establecemos que usuario va a ser el currentUser, servira tanto para 
hacer loguin, como para cuando creamos un usuario nuevo. Si podemos establecer un nuevo usuario, iremos al menu principal
desbloquearemos el boton dle menu login para ir al menu principal y en el menu de perfil estableceremos los datos de ese nuevo usuario */
    private bool startUser(UserProfile userLogged){
        bool startedCorrectly = false;
        if(userLogged != null){
            currentUser = userLogged;
            loginMenu.EnableBackButton();
            BackToMainMenu();
            profileMenu.setNameValue(currentUser.GetUserName());
            profileMenu.setMatchesWonValue(currentUser.GetMatchesWon());
            profileMenu.setMatchesLostValue(currentUser.GetMatchesLost());
            profileMenu.setEnemiesKilledValue(currentUser.GetEnemiesKilled());
            profileMenu.setDeathsValue(currentUser.GetDeaths());

            startedCorrectly = true;
        }else{
            Debug.Log("Menu Mediator: Start User: User is null");
            // ShowMessagePanel("Something went wrong, user could not be loaded correctly");
        }
        return startedCorrectly;
    }

    public UserProfile GetCurrentUser(){
        return currentUser;
    }

    public List<UserProfile> RetrieveRanking(string parameter){
        List<UserProfile> usersRanking = databaseMediator.getRankingProfiles(parameter);
        return usersRanking;
    }

    /* Metodos para actualizar datos cuando termine una partida */
    public void increaseMatchesWon(){
        currentUser.SetMatchesWon(currentUser.GetMatchesWon() + 1);
        databaseMediator.updateUser(currentUser, currentUser.GetUserName(), currentUser.GetUserPassword(), currentUser.GetMatchesWon(), currentUser.GetMatchesLost(), currentUser.GetEnemiesKilled(), currentUser.GetDeaths());
        profileMenu.setMatchesWonValue(currentUser.GetMatchesWon());
    }

    public void increaseMatchesLost(){
        currentUser.SetMatchesLost(currentUser.GetMatchesLost() + 1);
        databaseMediator.updateUser(currentUser, currentUser.GetUserName(), currentUser.GetUserPassword(), currentUser.GetMatchesWon(), currentUser.GetMatchesLost(), currentUser.GetEnemiesKilled(), currentUser.GetDeaths());
        profileMenu.setMatchesLostValue(currentUser.GetMatchesLost());
    }

    public void increaseEnemiesKilled(){
        currentUser.SetEnemiesKilled(currentUser.GetEnemiesKilled() + 1);
        databaseMediator.updateUser(currentUser, currentUser.GetUserName(), currentUser.GetUserPassword(), currentUser.GetMatchesWon(), currentUser.GetMatchesLost(), currentUser.GetEnemiesKilled(), currentUser.GetDeaths());
        profileMenu.setEnemiesKilledValue(currentUser.GetEnemiesKilled());
    }

    public void increaseDeaths(){
        currentUser.SetDeaths(currentUser.GetDeaths() + 1);
        databaseMediator.updateUser(currentUser, currentUser.GetUserName(), currentUser.GetUserPassword(), currentUser.GetMatchesWon(), currentUser.GetMatchesLost(), currentUser.GetEnemiesKilled(), currentUser.GetDeaths());
        profileMenu.setDeathsValue(currentUser.GetDeaths());
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
