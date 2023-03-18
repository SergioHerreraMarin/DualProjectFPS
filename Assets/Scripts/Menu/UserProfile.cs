using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Clase con la que manejaremos los usuarios del juego
estos o bien se crean en el menu CreateAccound
o bien con un login se cargaria desde la BBDD */
public class UserProfile : MonoBehaviour
{
    private string userName;
    private string userPassword;
    private int userScore;

    public string UserName { get => userName; set => userName = value; }
    public string UserPassword { get => userPassword; set => userPassword = value; }
    public int UserScore { get => userScore; set => userScore = value; }

    /* Este constructor seria para cargar un usuario desde la BBDD */
    public UserProfile(string userName, string userPassword, int userScore)
    {
        this.userName = userName;
        this.userPassword = userPassword;
        this.userScore = userScore;
    }

    /* Este constructor seria para crear un usuario nuevo */
        public UserProfile(string userName, string userPassword)
    {
        this.userName = userName;
        this.userPassword = userPassword;
        this.userScore = 0;
    }

}
