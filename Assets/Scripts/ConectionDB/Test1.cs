using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class Test1 : MonoBehaviour
{
    MySqlConnection connection;
    // Start is called before the first frame update
    void Start()
    {
        connection = new MySqlConnection("Server=localhost;Database=myDatabase;Uid=myUsername;Pwd=myPassword;");
        connection.Open();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void sampleQuery(){
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "Insert Into profiles (nameProfile, passwordProfile) Values ('testName', 'testPassword'))";
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Debug.Log(reader.GetString(0));
        }
        reader.Close(); 
    }
}
