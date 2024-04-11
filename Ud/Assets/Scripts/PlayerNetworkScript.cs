using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerNetworkScript : MonoBehaviourPunCallbacks
{
    public GameObject LocalXRRigGameobject;
    public GameObject AvatarHeadGameObject;
    public GameObject AvatarBodyGameObject;

    // Start is called before the first frame update
    void Start()
    {
        // Check if this PhotonView belongs to the local player
        if (photonView.IsMine)
        {
            // Activate the LocalXRRigGameobject for the local player
            LocalXRRigGameobject.SetActive(true);
            SetLayerRecursively(AvatarHeadGameObject,6);
            SetLayerRecursively(AvatarBodyGameObject,7);

           TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();
                if (teleportationAreas.Length > 0)
                {
                    Debug.Log("Found " + teleportationAreas.Length + " teleportation Area.");
                    foreach (var item in teleportationAreas)
                    {
                        // Assuming LocalXRRigGameobject is properly initialized
                        TeleportationProvider teleportationProvider = LocalXRRigGameobject.GetComponent<TeleportationProvider>();
                        if (teleportationProvider != null)
                        {
                            item.teleportationProvider = teleportationProvider;
                        }
                        else
                        {
                            Debug.LogError("TeleportationProvider component not found on LocalXRRigGameobject.");
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("No teleportation areas found.");
                }

        }
        else
        {
            // Deactivate the LocalXRRigGameobject for remote players
            LocalXRRigGameobject.SetActive(false);
            SetLayerRecursively(AvatarHeadGameObject,0);
            SetLayerRecursively(AvatarBodyGameObject,0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // You can add additional update logic here if needed
    }
        void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
