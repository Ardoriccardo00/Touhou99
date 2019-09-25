using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]

[RequireComponent(typeof(Player))]
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

    [HideInInspector] public static bool isServerPlayer;

    //PlayerUI[] uiList;

    void Start()
    {
        //uiList = FindObjectsOfType<PlayerUI>();

        //foreach (PlayerUI theUi in uiList)
        //{
        //    if (theUi.player.transform.name != transform.name)
        //    {
        //        theUi.gameObject.SetActive(false);
        //    }
        //}

        if (!isLocalPlayer)
        {
            DisableComponents();
        }

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
            ui.SetPlayer(GetComponent<Player>());               

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
        Player player = GameManager.GetPlayer(playerID);
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
        Player _player = GetComponent<Player>();

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
}
