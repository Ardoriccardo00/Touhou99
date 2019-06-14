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
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        playerMovement _player = GetComponent<playerMovement>();

        GameManager.RegisterPlayer(_netID, _player);
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    private void OnDisable()
    {
        GameManager.UnRegisterPlayer(transform.name);
    }
}
