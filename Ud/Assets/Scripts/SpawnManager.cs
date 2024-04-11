using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject GenericVRPrefab;

    public Vector3 Spawnpoint;

    private void Start() {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.Instantiate(GenericVRPrefab.name,Spawnpoint, Quaternion.identity);
        }
    }
}
