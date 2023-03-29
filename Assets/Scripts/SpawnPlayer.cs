using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject playerHUDPrefab;
    [SerializeField] private Transform spanwPosition;

    private void Start() {
        PhotonNetwork.Instantiate(playerPrefab.name, spanwPosition.position, Quaternion.identity);
    }


}
