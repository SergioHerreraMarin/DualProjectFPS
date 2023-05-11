using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonResponseClass : MonoBehaviour
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
