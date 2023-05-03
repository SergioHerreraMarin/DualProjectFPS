using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
// using static UnityEngine.JsonUtility;

/* Clase con los metodos para trabajar con la base de datos
las instancies de la clase Usuario de C# tendran nombre y los otros datos del usuario
el nombre sera unico para cada usuario y eso esta reflejado en la BBDD

En la BBDD cada usuario tiene una id, tambien unica y que sera el identificador, no obstante en
la clase usuario no guardaremos esta id, solo la usara la base de datos
los scripts i los usuarios que jueguen identificaran las cuentas por el nombre que sera unico*/
public class DatabaseMediator : MonoBehaviour
{
    [SerializeField] private MenuMediator menuMediator;
    [SerializeField] private PostUtils postUtils;
    MySqlConnection connection;
    MySqlCommand command;

    void Awake()
    {
        try{
            Debug.Log("DatabaseMediator: Awake");
            // Data to connect to a local server
            string server = "localhost";
            string dns_srv = "";
            string database = "db_dualproject";
            string uid = "root";
            string password = "localhost";

            //Data to connect to a remote server
            // string server = "mysql://root:5GsN2AKe60gIUrZroezw@containers-us-west-110.railway.app:6058/railway";
            // string server= "containers-us-west-110.railway.app";
            // string dns_srv =";dns-srv=true";
            // string database = "railway";
            // string uid = "root";
            // string password = "5GsN2AKe60gIUrZroezw";

            menuMediator.ConfigureDbMediator(this);
            connection = new MySqlConnection("Server=" +server +dns_srv+";Database="+database +";Uid="+uid +";Pwd="+password+";");
            connection.Open();
            // command = connection.CreateCommand();
            // command.CommandText = "CREATE TABLE IF NOT EXISTS profiles("+"idProfile INTEGER NOT NULL AUTO_INCREMENT,"+"nameProfile VARCHAR(255) UNIQUE,"
            //     +"passwordProfile VARCHAR(255),"+"scoreProfile INTEGER,"+"PRIMARY KEY(idProfile));";
            // command.ExecuteNonQuery();

            Debug.Log("DatabaseMediator: Awake: Will try to send a post request to the server");
            postUtils.SendPostRequest();
        }
        catch(Exception e)
        {
            Debug.Log("ERROR: DatabaseMediator: Awake: "+e);
            menuMediator.ShowMessagePanel("There has been an error setting the connection to the database, try to restart the game");
        }
        finally{
            if(connection != null){
            connection.Close();
            }
        }
    }

/* Dos funciones por separado para comprobar que existe el usuario y que dicho nombre de usuario 
 tiene dicha contraseña asociada
 se puede hacer una u otra comprovación segun se necesite*/
    public bool checkUserExists(string userName){
        Debug.Log("DatabaseMediator: checkUserExists");
        bool exists= false;
        try{
            connection.Open();
            command = connection.CreateCommand(); 
            command.CommandText = "SELECT * FROM profiles WHERE nameProfile = '" + userName + "';";
            MySqlDataReader reader = command.ExecuteReader();
            if(reader.Read() == true){
                exists =true;
            }
            reader.Close();
        }
        catch(Exception e){
            Debug.Log("ERROR: DatabaseMediator: checkUserExists: "+e);
            menuMediator.ShowMessagePanel("There has been an error connecting to the database to check if the user exists");
        }
        finally{
            if(connection != null){
            connection.Close();
            }
        }

        return exists;
    }

    public bool checkUserPassword(string userName, string password){
        Debug.Log("DatabaseMediator: checkUserPassword");
                bool exists= false;
        try{
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM profiles WHERE nameProfile = '" + userName + "' AND passwordProfile = '" + password + "';";
            MySqlDataReader reader = command.ExecuteReader();
            if(reader.Read() == true){
                exists =true;
            }
            reader.Close();
        }catch(Exception e){
            Debug.Log("ERROR: DatabaseMediator: checkUserPassword: "+e);
            menuMediator.ShowMessagePanel("There has been an error connecting to the database to check the user password");
        }
        finally{
            if(connection != null){
            connection.Close();
            }
        }
        return exists;
    }

/* Insertar un usuario, sera llamada cuando se cree un usuario */
    public void insertNewUser(UserProfile userToInsert){
        Debug.Log("DatabaseMediator: insertNewUser");
        try{
            string userName = userToInsert.UserName;
            string password = userToInsert.UserPassword;

            Debug.Log("We will insert this user: "+userToInsert.toString());
            connection.Open();
            command = connection.CreateCommand(); 
            command.CommandText = "INSERT INTO profiles (nameProfile, passwordProfile, scoreProfile) VALUES ('" + userName + "', '" + password + "', '" + 0 +"');";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Debug.Log(reader.GetString(0));
            }
            reader.Close(); 
        }catch(Exception e){
            Debug.Log("ERROR: DatabaseMediator: insertNewUser: "+e);
            menuMediator.ShowMessagePanel("There has been an error creating a new user in the database");
        }
        finally{
            if(connection != null){
            connection.Close();
            }
        }
    }

    public void insertNewUser(string userName, string userPassword, bool loggedIn, int userScore){
        Debug.Log("DatabaseMediator: insertNewUser");

        try{
            Debug.Log("We will insert a user with name"+userName+" and password "+userPassword);
            connection.Open();
            command = connection.CreateCommand(); 
            command.CommandText = "INSERT INTO profiles (nameProfile, passwordProfile, isLogged, scoreProfile) VALUES ('" + userName + "', '" + userPassword + "', '" + loggedIn + "', '" + userScore +"');";
            int rowsAffected = command.ExecuteNonQuery();
            Debug.Log(rowsAffected + " row(s) affected.");
        }catch(Exception e){
            Debug.Log("ERROR: DatabaseMediator: insertNewUser: "+e);
            menuMediator.ShowMessagePanel("There has been an error creating a new user in the database");
        }
        finally{
            if(connection != null){
            connection.Close();
            }
        }
    }

/* Para buscar un usuario, se usara para el login, comprobar que existe y de ser asi, 
devolvernos el objeto usuario. 
El booleano es para indicarle si estamos sacando info del usuario
para hacer un login, pues en este caso hay que hacer una comprobacion extra 
reader.IsDBNull(4) se refiere a la columna provisional de puntuaciones*/
    public UserProfile retrieveUserByName(bool login, string userName){
        Debug.Log("DatabaseMediator: retrieveUserByName");
        UserProfile retrievedUser = null;

        try{
            connection.Open();
            command = connection.CreateCommand(); 
            command.CommandText = "SELECT * FROM profiles WHERE nameProfile = '" + userName + "';";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (!reader.IsDBNull(0) && !reader.IsDBNull(1) && !reader.IsDBNull(2) && !reader.IsDBNull(4))
                {
                    Debug.Log("****"+reader.GetString(0)+" "+ reader.GetString(1) +" "+reader.GetString(2) +" "+reader.GetString(3) +" "+reader.GetString(4));
                    if(login == true)
                    {
                        if(reader.GetString(3) == "False"){
                            retrievedUser = new UserProfile(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                        }else{
                            Debug.Log("You can't logg with this user, it is already logged in");
                            menuMediator.ShowMessagePanel("You can't logg with this user, it is already logged in");
                        }
                    }else{
                        retrievedUser = new UserProfile(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(4));
                    }
                }else if(reader.IsDBNull(4))
                {
                    Debug.Log(reader.GetString(0)+" "+ reader.GetString(1) +" "+reader.GetString(2) +" "+0);
                    retrievedUser = new UserProfile(reader.GetString(0), reader.GetString(1), reader.GetString(2), 0);
                }
            }
            reader.Close(); 
            Debug.Log("Retrieved user: " + retrievedUser?.toString());
        }catch(Exception e){
            Debug.Log("ERROR: DatabaseMediator: retrieveUserByName: "+e);
            menuMediator.ShowMessagePanel("There has been an error getting the user information from the database");
        }
        finally{
            if(connection != null){
            connection.Close();
            }
        }

        if(login == true)
        {
            if(retrievedUser != null)
            SetUserLogged(retrievedUser.getUserName());
        }
        
        return retrievedUser;
    }

    public UserProfile retrieveUserById(bool login, string userId){
        Debug.Log("DatabaseMediator: retrieveUserById");
        UserProfile retrievedUser = null;

        try{
            connection.Open();
            command = connection.CreateCommand(); 
            command.CommandText = "SELECT * FROM profiles WHERE idProfile = '" + userId + "';";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (!reader.IsDBNull(0) && !reader.IsDBNull(1) && !reader.IsDBNull(2) && !reader.IsDBNull(4))
                {
                    Debug.Log("****"+reader.GetString(0)+" "+ reader.GetString(1) +" "+reader.GetString(2) +" "+reader.GetString(3) +" "+reader.GetString(4));
                    if(login == true)
                    {
                        if(reader.GetString(3) == "False"){
                            retrievedUser = new UserProfile(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                        }else{
                            Debug.Log("You can't logg with this user, it is already logged in");
                            menuMediator.ShowMessagePanel("You can't logg with this user, it is already logged in");
                        }
                    }else{
                        retrievedUser = new UserProfile(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(4));
                    }
                }else if(reader.IsDBNull(4))
                {
                    Debug.Log(reader.GetString(0)+" "+ reader.GetString(1) +" "+reader.GetString(2) +" "+0);
                    retrievedUser = new UserProfile(reader.GetString(0), reader.GetString(1), reader.GetString(2), 0);
                }
            }
            reader.Close(); 

        }catch(Exception e){
            Debug.Log("ERROR: DatabaseMediator: retrieveUserById: "+e);
            menuMediator.ShowMessagePanel("There has been an error getting the user information from the database");
        }
        finally{
            if(connection != null){
            connection.Close();
            }
        }

        Debug.Log("Retrieved user: " + retrievedUser?.toString());
        if(login == true)
        {
            if(retrievedUser != null)
            SetUserLogged(retrievedUser.getUserName());
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
        Debug.Log("DatabaseMediator: updateUser");
        try{
            if(newName == "") newName = userToUpdate.getUserName();
            if(newPassword == "") newPassword = userToUpdate.getUserPassword();
            if(newScore == 0) newScore = userToUpdate.getUserScore();

            connection.Open();
            command = connection.CreateCommand(); 
            command.CommandText = "UPDATE profiles SET nameProfile = '" + newName + "', passwordProfile = '" + newPassword + "', scoreProfile = '" + newScore 
            + "' WHERE idProfile = '" + userToUpdate.getUserId() + "' AND nameProfile = '" + userToUpdate.getUserName() + "';";

            Debug.Log("We will update this user: "+userToUpdate.toString()+" \nwith this new data: "+newName+" "+newPassword+" "+newScore+"");

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0) 
            { 
                Debug.Log("User not found at the database, it will be recreated with the udapted information from the game");
                recreateUser = true;
            } 

        }catch(Exception e){
            Debug.Log("ERROR: DatabaseMediator: updateUser: "+e);
            menuMediator.ShowMessagePanel("There has been an error updating the user in the database");
        }
        finally{
            if(connection != null){
            connection.Close();
            }
        }

        if(recreateUser){
            insertNewUser(newName, newPassword, true, newScore);
        }

        updatedUser = retrieveUserByName(false, newName);
        return updatedUser;
    }

/* Eliminar un usuario de la base de datos, de momento el menu no tiene esta opcion */
    public bool deleteUser(UserProfile userToDelete){
        Debug.Log("DatabaseMediator: deleteUser");
        string userName = userToDelete.UserName;
        bool deleted = false;
        try{
            Debug.Log("We will delete this user: "+userToDelete.toString());
            connection.Open();
            command = connection.CreateCommand(); 
            command.CommandText = "DELETE FROM profiles WHERE nameProfile = '" + userName  +"';";
            MySqlDataReader reader = command.ExecuteReader();
            if(reader.RecordsAffected > 0){
                deleted = true;
            }
            while (reader.Read())
            {
                Debug.Log(reader.GetString(0));
            }
            reader.Close(); 
        }catch(Exception e){
            Debug.Log("ERROR: DatabaseMediator: deleteUser: "+e);
            menuMediator.ShowMessagePanel("There has been an error in the database deleting the user");
        }
        finally{
            if(connection != null){
            connection.Close();
            }
        }
 
        return deleted;
    }

    public void SetUserLogged(string nameProfile){
        Debug.Log("DatabaseMediator: setUserLogged");
        try{
            connection.Open();
            command = connection.CreateCommand(); 
            command.CommandText = "UPDATE profiles SET isLogged = TRUE WHERE nameProfile = '" + nameProfile + "';";
            command.ExecuteNonQuery();
        }catch(Exception e){
            Debug.Log("ERROR: DatabaseMediator: setUserLogged: "+e);
            menuMediator.ShowMessagePanel("There has been an error loging the user and retrieving the data from the database");
        }
        finally{
            if(connection != null){
            connection.Close();
            }
        }
    }

    public void SetUserLoggedOut(string nameProfile){
        Debug.Log("DatabaseMediator: setUserLoggedOut");
        try{
            connection.Open();
            command = connection.CreateCommand(); 
            command.CommandText = "UPDATE profiles SET isLogged = FALSE WHERE nameProfile = '" + nameProfile + "';";
            command.ExecuteNonQuery();
        }catch(Exception e){
            Debug.Log("ERROR: DatabaseMediator: setUserLoggedOut: "+e);
            menuMediator.ShowMessagePanel("There has been an error logging out the user in the database");
        }
        finally{
            if(connection != null){
            connection.Close();
            }
        }
    }

/* Metodo para obtener la id a partir del nombre */
    private string getId(string userName){
        Debug.Log("DatabaseMediator: getId");
        string id = "";
        try{
            connection.Open();
            command = connection.CreateCommand(); 
            command.CommandText = "SELECT idProfile FROM profiles WHERE nameProfile = '" + userName + "';";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Debug.Log(reader.GetString(0));
                id = reader.GetString(0);
            }
            reader.Close(); 
        }catch(Exception e){
            Debug.Log("ERROR: DatabaseMediator: getId: "+e);
            menuMediator.ShowMessagePanel("There has been an error connecting to the database");
        }
        finally{
            if(connection != null){
            connection.Close();
            }
        }
        return id;
    }

/* Por si queremos resetear la base de datos */
    public void resetDatabase(){
        Debug.Log("DatabaseMediator: resetDatabase");
        try{
            connection.Open();
            command = connection.CreateCommand(); 
            command.CommandText = "SET SQL_SAFE_UPDATES = 0;"
                                + "DELETE FROM profiles WHERE 1;"
                                + "SET SQL_SAFE_UPDATES = 1;";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Debug.Log(reader.GetString(0));
            }
            reader.Close(); 
        }catch(Exception e){
            Debug.Log("ERROR: DatabaseMediator: resetDatabase: "+e);
            menuMediator.ShowMessagePanel("There has been an error resetting the database");
        }
        finally{
            if(connection != null){
            connection.Close();
            }
        }
    }

/* Nos crea de 0 la base de datos, con unos valores de prueba */
    public void testDatabase(){
        Debug.Log("DatabaseMediator: testDatabase");
        try{
            connection.Open();
            command = connection.CreateCommand(); 
            command.CommandText = "DROP TABLE IF EXISTS profiles;"

                                + "CREATE TABLE profiles(idProfile INTEGER NOT NULL AUTO_INCREMENT,"
                                + "nameProfile VARCHAR(255) UNIQUE NOT NULL," 
                                + "passwordProfile VARCHAR(255) NOT NULL,"
                                + "scoreProfile INTEGER,"
                                + "PRIMARY KEY(idProfile));"

                                + "Insert Into profiles (nameProfile, passwordProfile, scoreProfile) Values ('test', 'test', '10');" 
                                + "Insert Into profiles (nameProfile, passwordProfile, scoreProfile) Values ('test2', 'test2', '25');"
                                + "Insert Into profiles (nameProfile, passwordProfile, scoreProfile) Values ('test3', 'test3', '15');";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Debug.Log(reader.GetString(0));
            }
            reader.Close(); 
        }catch(Exception e){
            Debug.Log("ERROR: DatabaseMediator: testDatabase: "+e);
            menuMediator.ShowMessagePanel("There has been an error with the trial insert into the database");
        }
        finally{
            if(connection != null){
            connection.Close();
            }
        }
    }
}
