using UnityEngine;
using Photon.Pun;

public class RoundWalls : MonoBehaviourPun
{
    [PunRPC]
    public void DesactiveWalls()
    {   
        gameObject.SetActive(false);
    }

    [PunRPC]
    public void ActiveWalls()
    {
        gameObject.SetActive(true);
    }



}
