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

    public GameObject cameraPrefab;
    //private Transform cameraSpawnPoint;
    public Transform player;
    private GameObject playerCamera;

    void Start()
    {
        player = GetComponent<Transform>();
        if (!isLocalPlayer)
        {
            AssignRemoteLayer();  
        }
        RegisterPlayer();
        //PositionCamera();
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

    void PositionCamera()
    {
        playerCamera = Instantiate(cameraPrefab, new Vector3(player.position.x, player.position.y, -3), player.rotation);
    }
}
