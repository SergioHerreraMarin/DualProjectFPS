using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseMediator_NodeJS : MonoBehaviour
{
    [SerializeField] private MenuMediator menuMediator;
    [SerializeField] private PostUtils postUtils;

    /* Remote Connection */
    // public static string protocol = "https";
    // public static int port = 443;
    // public static string domain = "nodejsdualproject-production.up.railway.app";

    /* Local Connection */ 
    public static string protocol = "https";
    public static int port = 3001;
    public static string domain = "localhost";

    private string serverUrl = protocol + "://" + domain + ":" + port;

    void Awake(){
        Debug.Log("DatabaseMediator_NodeJS: Awake: Will try to send a post request to the server");
        // postUtils.SendPostRequest();
    }

    /* Dos funciones por separado para comprobar que existe el usuario y que dicho nombre de usuario 
 tiene dicha contraseña asociada
 se puede hacer una u otra comprovación segun se necesite*/
public bool CheckUserExists(string userName)
{
    return false;
}


    public bool checkUserPassword(string userName, string password){
        Debug.Log("DatabaseMediator_NodeJS: checkUserPassword");
        bool exists= false;
        try{
        }catch(Exception e){
        }
        finally{
        }
        return exists;
    }

/* Insertar un usuario, sera llamada cuando se cree un usuario */
    public void insertNewUser(UserProfile userToInsert){
        Debug.Log("DatabaseMediator_NodeJS: insertNewUser");
        try{
            // postUtils.SendPostRequest();
        }catch(Exception e){
            Debug.Log("ERROR: DatabaseMediator_NodeJS: insertNewUser: "+e);
            menuMediator.ShowMessagePanel("There has been an error creating a new user in the database");
        }
        finally{
        }
    }

    public void insertNewUser(string userName, string userPassword, bool loggedIn, int userScore){
        Debug.Log("DatabaseMediator_NodeJS: insertNewUser");

        try{
            // postUtils.SendPostRequest();
        }catch(Exception e){
        }
        finally{
        }
    }

/* Para buscar un usuario, se usara para el login, comprobar que existe y de ser asi, 
devolvernos el objeto usuario. 
El booleano es para indicarle si estamos sacando info del usuario
para hacer un login, pues en este caso hay que hacer una comprobacion extra 
reader.IsDBNull(4) se refiere a la columna provisional de puntuaciones*/
    public UserProfile retrieveUserByName(bool login, string userName){
        Debug.Log("DatabaseMediator_NodeJS: retrieveUserByName");
        UserProfile retrievedUser = null;

        try{
            
        }catch(Exception e){
        }
        finally{
        }

        if(login == true)
        {
            if(retrievedUser != null)
            SetUserLogged(retrievedUser.GetUserName());
        }
        
        return retrievedUser;
    }

    public UserProfile retrieveUserById(bool login, string userId){
        Debug.Log("DatabaseMediator_NodeJS: retrieveUserById");
        UserProfile retrievedUser = null;

        try{
            
        }catch(Exception e){
        }
        finally{
        }

        Debug.Log("Retrieved user: " + retrievedUser?.ToString());
        if(login == true)
        {
            if(retrievedUser != null)
            SetUserLogged(retrievedUser.GetUserName());
        }

        return retrievedUser;
    }

/* Actualizar un usuario, por ejemplo una nueva puntuación, la contraseña o incluso el nombre
Se actualizaran todos los valores, aunque no siempre se quieran cambiar todos,
en este caso el update se hara con los mismos valores y en la practica no cambiara 

Si no encontrara al usuario (se ha borrado de la BBDD por cualquier error) lo volveria a crear*/
    public UserProfile updateUser(UserProfile userToUpdate, string newName, string newPassword, int newScore){
        bool recreateUser = false;
        UserProfile updatedUser = userToUpdate;
        Debug.Log("DatabaseMediator_NodeJS: updateUser");
        try{
            
        }catch(Exception e){
        }
        finally{
        }

        if(recreateUser){
            insertNewUser(newName, newPassword, true, newScore);
        }

        updatedUser = retrieveUserByName(false, newName);
        return updatedUser;
    }

/* Eliminar un usuario de la base de datos, de momento el menu no tiene esta opcion */
    public bool deleteUser(UserProfile userToDelete){
        Debug.Log("DatabaseMediator_NodeJS: deleteUser");
        string userName = userToDelete.GetUserName();
        bool deleted = false;
        try{
            
        }catch(Exception e){
        }
        finally{
        }
 
        return deleted;
    }

    public void SetUserLogged(string nameProfile){
        Debug.Log("DatabaseMediator_NodeJS: setUserLogged");
        try{
            
        }catch(Exception e){
        }
        finally{
        }
    }

    public void SetUserLoggedOut(string nameProfile){
        Debug.Log("DatabaseMediator_NodeJS: setUserLoggedOut");
        try{
            
        }catch(Exception e){
        }
        finally{
        }
    }

/* Metodo para obtener la id a partir del nombre */
    private string getId(string userName){
        Debug.Log("DatabaseMediator_NodeJS: getId");
        string id = "";
        try{
            
        }catch(Exception e){
        }
        finally{
        }
        return id;
    }

/* Por si queremos resetear la base de datos */
    public void resetDatabase(){
        Debug.Log("DatabaseMediator_NodeJS: resetDatabase");
        try{
            
        }catch(Exception e){
        }
        finally{
        }
    }

/* Nos crea de 0 la base de datos, con unos valores de prueba */
    public void testDatabase(){
        Debug.Log("DatabaseMediator_NodeJS: testDatabase");
        try{
            
        }catch(Exception e){
        }
        finally{
        }
    }
}

[Serializable]
public class JsonRequestClass
{
    public int idProfile { get; set; }
    public string nameProfile { get; set; }
    public string passwordProfile { get; set; }
    public bool isLogged { get; set; }
    public int scoreProfile { get; set; }
    public bool boolRequest { get; set; }

    public JsonRequestClass(int idProfile, string nameProfile, string passwordProfile, bool isLogged, int scoreProfile, bool boolRequest)
    {

        this.idProfile = idProfile;
        this.nameProfile = nameProfile;
        this.passwordProfile = passwordProfile;
        this.isLogged = isLogged;
        this.scoreProfile = scoreProfile;
        this.boolRequest = boolRequest;
    }

    public JsonRequestClass(){
    }
}

[Serializable]
public class JsonResponseClass
{
    public string status { get; set; }
    public string message { get; set; }
    public string result { get; set; }
    public bool responseBool { get; set; }

    public JsonResponseClass(string status, string message, string result, bool responseBool)
    {
        this.status = status;
        this.message = message;
        this.result = result;
        this.responseBool = responseBool;
    }

    public JsonResponseClass(){
    }
}

