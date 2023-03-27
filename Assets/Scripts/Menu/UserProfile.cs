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
    private string userScore;

    public string UserId { get => userId; }
    public string UserName { get => userName; set => userName = value; }
    public string UserPassword { get => userPassword; set => userPassword = value; }
    public string UserScore { get => userScore; set => userScore = value; }

    /* Este constructor seria para cargar un usuario desde la BBDD */
    public UserProfile(string userId, string userName, string userPassword, string userScore)
    {
        this.userId = userId;
        this.userName = userName;
        this.userPassword = userPassword;
        this.userScore = userScore;
    }

    /* Este constructor seria para crear un usuario nuevo */
    public UserProfile(string userName, string userPassword)
    {
        this.userName = userName;
        this.userPassword = userPassword;
        this.userScore = "0";
    }

    public string toString(){
        return "id: "+ userId + "User: " + userName + " Password: " + userPassword + " Score: " + userScore;
    }

}
