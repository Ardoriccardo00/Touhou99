using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]

[RequireComponent(typeof(playerMovement))]
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    string remoteLayerName = "RemotePlayer";
    void Start()
    {
        if (!isLocalPlayer)
        {
            AssignRemoteLayer();  
        }
        RegisterPlayer();
    }

    void RegisterPlayer()
    {
        string _ID = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }
}
