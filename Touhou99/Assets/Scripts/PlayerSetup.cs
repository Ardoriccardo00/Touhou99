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

    [SerializeField]
    GameObject girlSwitcherPrefab;
    private GameObject girlSwitcherInstance;

    public GameObject cameraPrefab;
    //private Transform cameraSpawnPoint;
    public Transform player;
    private GameObject playerCamera;

    public string thisPlayerUsername;

    void Start()
    {
        if (isServer)
            Debug.Log("server");
        else
            Debug.Log("Client");

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

        //girlSwitcherInstance = Instantiate(girlSwitcherPrefab);
        //GirlSwitcher gs = ui.GetComponent<GirlSwitcher>();
        //print(gs);
        //if (gs = null)
        //    Debug.LogError("no Girl chooser on playerui prefab");
        //gs.SetPlayer(GetComponent<playerMovement>());

        //girlSwitcherInstance = FindObjectOfType<GirlSwitcher>();
        //girlSwitcherInstance.SetPlayer(GetComponent<playerMovement>());

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
        GameManager.AddAlivePlayer(_netID, _player);
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

    //[Command]
    //public void CmdSetAuth(NetworkInstanceId objectId, NetworkIdentity player)
    //{
    //    GameObject iObject = NetworkServer.FindLocalObject(objectId);
    //    NetworkIdentity networkIdentity = iObject.GetComponent<NetworkIdentity>();

    //    //Checks if anyone else has authority and removes it and lastly gives the authority to the player who interacts with object
    //    NetworkConnection otherOwner = networkIdentity.clientAuthorityOwner;
    //    if (otherOwner == player.connectionToClient)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        if (otherOwner != null)
    //        {
    //            networkIdentity.RemoveClientAuthority(otherOwner);
    //        }
    //        networkIdentity.AssignClientAuthority(player.connectionToClient);
    //    }

    //    networkIdentity.AssignClientAuthority(player.connectionToClient);
    //}
}
