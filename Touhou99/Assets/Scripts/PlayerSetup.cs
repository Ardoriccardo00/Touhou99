using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]

[RequireComponent(typeof(playerMovement))]
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    GameObject playerUIPrefab;
    private GameObject playerUIInstance;

    public GameObject cameraPrefab;
    //private Transform cameraSpawnPoint;
    public Transform player;
    private GameObject playerCamera;

    void Start()
    {
        player = GetComponent<Transform>();
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();  
        }

        playerUIInstance = Instantiate(playerUIPrefab);
        playerUIInstance.name = playerUIPrefab.name;

        PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
        if (ui == null)
            Debug.LogError("no playerUI on playerui prefab");
        ui.SetPlayer(GetComponent<playerMovement>());

        string _username = "loading...";
        if (UserAccountManager.IsLoggedIn)
            _username = UserAccountManager.LoggedIn_Username;
        else
            _username = transform.name;

        CmdSetUserName(transform.name, _username);
    }

    [Command]
    void CmdSetUserName(string playerID, string username)
    {
        playerMovement player = GameManager.GetPlayer(playerID);
        if(player != null)
        {
            Debug.Log(username + " Has joined");
            player.username = username;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        playerMovement _player = GetComponent<playerMovement>();

        GameManager.RegisterPlayer(_netID, _player);
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }


    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    private void OnDisable()
    {
        Destroy(playerUIInstance);
        GameManager.UnRegisterPlayer(transform.name);
    }
}
