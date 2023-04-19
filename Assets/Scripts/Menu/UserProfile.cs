using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Clase con la que manejaremos los usuarios del juego
estos o bien se crean en el menu CreateAccound
o bien con un login se cargaria desde la BBDD */
public class UserProfile 
{
    /* La id de usuario no se podra modificar una vez cargada de la BBDD al ser un identificador unico 
    que debe permanecer igual incluso si la cuenta se cambia el nombre, 
    el resto de datos si podran ser modificados */

    private readonly string userId;
    private string userName;
    private string userPassword;
    /* Dato provisional, para probar con persistencia de datos */
    private int userScore;

    public string UserId { get => userId; }
    public string UserName { get => userName; set => userName = value; }
    public string UserPassword { get => userPassword; set => userPassword = value; }
    public int UserScore { get => userScore; set => userScore = value; }

    /* Este constructor se usara para cargar un usuario desde la BBDD,
    para evitar posibles inconsistencias de datos, en nuestro codigo del juego,
    los objetos usuarios solo se instanciaran a partir de informacion de la BBDD
    
    Cuando se crea un usuario en el juego, primero se crea y se guarda en la base de datos y para
    establecerlo como usuario actual del juego, cogeremos de la BBDD ese usuario recien creado, 
    de esta forma todo usuario esta respaldado por su presencia previa en la BBDD */
    public UserProfile(string userId, string userName, string userPassword, int userScore)
    {
        this.userId = userId;
        this.userName = userName;
        this.userPassword = userPassword;
        this.userScore = userScore;
    }

    //Generate me the setters and getters for this class

    public string getUserId(){
        return userId;
    }

    public string getUserName(){
        return userName;
    }

    public void setUserName(string userName){
        this.userName = userName;
    }

    public string getUserPassword(){
        return userPassword;
    }

    public void setUserPassword(string userPassword){
        this.userPassword = userPassword;
    }

    public int getUserScore(){
        return userScore;
    }

    public void setUserScore(int userScore){
        this.userScore = userScore;
    }

    public string toString(){
        return "id: "+ userId + "User: " + userName + " Password: " + userPassword + " Score: " + userScore;
    }



}
