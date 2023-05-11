using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PruebaPeticion : MonoBehaviour
{
    // Start is called before the first frame update
    string prueba;
    void Start()
    {
        StartCoroutine(SendPostRequest());
    }

    private IEnumerator SendPostRequest()
    {
        // URL del servidor y datos del formulario
        string url = "localhost:3000/api/get_ranking";
        WWWForm form = new WWWForm();
        form.AddField("username", "John");
        form.AddField("password", "123456");

        // Crear una solicitud POST con UnityWebRequest
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // Enviar la solicitud y esperar la respuesta
        yield return request.SendWebRequest();

        // Comprobar si hubo algún error
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error en la solicitud: " + request.error);
        }
        else
        {
            // La solicitud fue exitosa
            Debug.Log("Solicitud exitosa!");

            // Obtener la respuesta del servidor
            string response = request.downloadHandler.text;
            prueba = response;
            Debug.Log(prueba);

            //MyResponseData responseData = JsonUtility.FromJson<MyResponseData>(response);
        }
    }


}
