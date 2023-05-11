using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonRequestClass : MonoBehaviour
{
    public int idProfile { get; set; }
    public string nameProfile { get; set; }
    public string passwordProfile { get; set; }
    public bool isLogged { get; set; }
    public int scoreProfile { get; set; }
    public bool boolRequest { get; set; }

    public JsonRequestClass(int idProfile, string nameProfile, string passwordProfile, bool isLogged, int scoreProfile, bool boolRequest)
    {

        this.idProfile = idProfile;
        this.nameProfile = nameProfile;
        this.passwordProfile = passwordProfile;
        this.isLogged = isLogged;
        this.scoreProfile = scoreProfile;
        this.boolRequest = boolRequest;
    }

    public JsonRequestClass(){
    }
}
