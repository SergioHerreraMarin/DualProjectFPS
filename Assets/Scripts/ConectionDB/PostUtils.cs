using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PostUtils : MonoBehaviour
{
    // Set the URL of your Node.js server here
    private static string protocol = "https";
    private static int port = 443;
    // private static string endPoint = "/api/testQuery";
    private static string domain = "nodejsdualproject-production.up.railway.app";

    private string serverUrl = protocol + "://" + domain + ":" + port;


    // url to connect in local
    // private string serverUrl = "http://localhost:3000/api/testQuery";

    // Call this function to send the POST request
    public string SendPostRequest(string endPoint, string jsonInput)
    {
        Debug.Log("PostUtils: SendPostRequest to " + serverUrl + "");
        Debug.Log(StartCoroutine(PostRequestCoroutine(endPoint, jsonInput)));
        // Debug.Log("PostUtils: SendPostRequest: returned: " + returnedString);
        return "returnedString";
    }

    IEnumerator PostRequestCoroutine(string endPoint, string jsonInput)
    {
        // Create the form data to send in the request
        WWWForm form = new WWWForm();
        form.AddField("jsonInput", jsonInput);

        // Send the request
        UnityWebRequest request = UnityWebRequest.Post(serverUrl + endPoint, form);
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // Print the response from the server
            Debug.Log(request.downloadHandler.text);
            yield return request.downloadHandler.text;
        }
    }
}

