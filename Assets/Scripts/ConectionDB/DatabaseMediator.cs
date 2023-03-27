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
    MySqlConnection connection;

    // Start is called before the first frame update
    void Start()
    {
        connection = new MySqlConnection("Server=localhost;Database=db_dualproject;Uid=root;Pwd=localhost;");
        connection.Open();
    }

/* Insertar un usuario, sera llamada cuando se cree un usuario */
    void insertNewUser(UserProfile userToInsert){
        MySqlCommand command = connection.CreateCommand();
        string userName = userToInsert.UserName;
        string password = userToInsert.UserPassword;

        Debug.Log("We will insert this user: "+userToInsert.toString());
        command.CommandText = "INSERT INTO profiles (nameProfile, passwordProfile) VALUES ('" + userName + "', '" + password + "');";
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Debug.Log(reader.GetString(0));
        }
        reader.Close(); 

    }

/* Para buscar un usuario, se usara para el login, comprobar que existe y de ser asi, darnos la info del usuario */
    UserProfile retrieveUser(string userName){
        UserProfile retrievedUser = null;
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT nameProfile, passwordProfile, scoreProfile FROM profiles WHERE nameProfile = '" + userName + ";";
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Debug.Log(reader.GetString(0));
            retrievedUser = new UserProfile(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
        }
        reader.Close(); 
        Debug.Log("Retrieved user: " + retrievedUser.toString());

        return retrievedUser;
    }

/* Actualizar un usuario, por ejemplo una nueva puntuación, la contraseña o incluso el nombre */
    void updateUser(UserProfile userToUpdate){
        string id = getId(userToUpdate.UserName);
        string userName = userToUpdate.UserName;
        string password = userToUpdate.UserPassword;
        string score = userToUpdate.UserScore;

        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "UPDATE profiles SET nameProfile = '" + userName + "', passwordProfile = '" + password + "', scoreProfile = '" + score + "' WHERE idProfile = '" + id + "';";

        Debug.Log("We will update this user: "+userToUpdate.toString());

        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Debug.Log(reader.GetString(0));
        }
        reader.Close(); 
    }

/* Eliminar un usuario de la base de datos, de momento el menu no tiene esta opcion */
    void deleteUser(UserProfile userToDelete){
        MySqlCommand command = connection.CreateCommand();
        string userName = userToDelete.UserName;
        string password = userToDelete.UserPassword;

        Debug.Log("We will delete this user: "+userToDelete.toString());

        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Debug.Log(reader.GetString(0));
        }
        reader.Close(); 
    }

/* Metodo para obtener la id a partir del nombre */
    private string getId(string userName){
        string id = "";
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT idProfile FROM profiles WHERE nameProfile = '" + userName + ";";
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Debug.Log(reader.GetString(0));
            id = reader.GetString(0);
        }
        reader.Close(); 
        return id;
    }

/* Por si queremos resetear la base de datos */
    void resetDatabase(){
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SET SQL_SAFE_UPDATES = 0;"
                            + "DELETE FROM profiles WHERE 1;"
                            + "SET SQL_SAFE_UPDATES = 1;";
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Debug.Log(reader.GetString(0));
        }
        reader.Close(); 
    }

/* Nos crea de 0 una base de datos de prueba */
    void testDatabase(){
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "DROP TABLE IF EXISTS profiles;"

                            + "CREATE TABLE profiles(idProfile INTEGER NOT NULL AUTO_INCREMENT,"
                            + "nameProfile VARCHAR(255) UNIQUE," 
                            + "passwordProfile VARCHAR(255),"
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
    }
}
