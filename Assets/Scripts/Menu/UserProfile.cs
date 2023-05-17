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
    private int matchesWon;
    private int matchesLost;
    private int enemiesKilled;
    private int deaths;

    /* Este constructor se usara para cargar un usuario desde la BBDD,
    para evitar posibles inconsistencias de datos, en nuestro codigo del juego,
    los objetos usuarios solo se instanciaran a partir de informacion de la BBDD
    
    Cuando se crea un usuario en el juego, primero se crea y se guarda en la base de datos y para
    establecerlo como usuario actual del juego, cogeremos de la BBDD ese usuario recien creado, 
    de esta forma todo usuario esta respaldado por su presencia previa en la BBDD */
    public UserProfile(string userId, string userName, string userPassword, int matchesWon, int matchesLost, int enemiesKilled, int deaths)
    {
        this.userId = userId;
        this.userName = userName;
        this.userPassword = userPassword;
        this.matchesWon = matchesWon;
        this.matchesLost = matchesLost;
        this.enemiesKilled = enemiesKilled;
        this.deaths = deaths;
    }

    //Generate me the setters and getters for this class

    public string GetUserId(){
        return userId;
    }

    public string GetUserName(){
        return userName;
    }

    public string GetUserPassword(){
        return userPassword;
    }

    public int GetMatchesWon(){
        return matchesWon;
    }

    public int GetMatchesLost(){
        return matchesLost;
    }

    public int GetEnemiesKilled(){
        return enemiesKilled;
    }

    public int GetDeaths(){
        return deaths;
    }

    public void SetUserName(string userName){
        this.userName = userName;
    }

    public void SetUserPassword(string userPassword){
        this.userPassword = userPassword;
    }

    public void SetMatchesWon(int matchesWon){
        this.matchesWon = matchesWon;
    }

    public void SetMatchesLost(int matchesLost){
        this.matchesLost = matchesLost;
    }

    public void SetEnemiesKilled(int enemiesKilled){
        this.enemiesKilled = enemiesKilled;
    }

    public void SetDeaths(int deaths){
        this.deaths = deaths;
    }

/* Metodos a los que recurrir para entrar estadisticas del nuevo juego */
    public void AddMatchWon(){
        matchesWon++;
    }

    public void AddMatchLost(){
        matchesLost++;
    }

    public void AddEnemiesKilled(int enemiesKilledMatch){
        enemiesKilled = enemiesKilled + enemiesKilledMatch;
    }

    public void AddDeaths(int deathsMatch){
        deaths = deaths + deathsMatch;
    }

    public string ToString(){
        return "id: "+ userId + "User: " + userName + " Password: " + userPassword + " Matches Won: " + matchesWon +" Matches Lost: " + matchesLost + " Enemies Killed: " + enemiesKilled + " Deaths: " + deaths;
    }

}
