using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{
    [Header("Components")]
    [SerializeField] Behaviour[] componentsToDisable;
    [SerializeField] string remoteLayerName = "RemotePlayer";
    [SerializeField] GameObject playerUIPrefab;
    private Transform player;

    private GameObject playerUIInstance;
    private GameObject girlSwitcherInstance;
    private GameObject playerCamera;
    [HideInInspector] public static bool isServerPlayer;
    private GameObject uiContainer;

    void Start()
    {
        player = GetComponent<Transform>();

        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }

        SetUI();
        SetUsername();
    }

    private void SetUsername()
    {
        string _username = "loading...";
        if (UserAccountManager.IsLoggedIn)
            _username = UserAccountManager.LoggedIn_Username;
        else
            _username = transform.name;

        CmdSetUserName(transform.name, _username);
    }

    private void SetUI()
    {
        playerUIInstance = Instantiate(playerUIPrefab);
        playerUIInstance.name = playerUIPrefab.name;
        //playerUIInstance.transform.SetParent(uiContainer.transform);

        PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();

        if (ui == null)
            Debug.LogError("no playerUI on playerui prefab");
        ui.SetPlayer(GetComponent<Player>());
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
        GameManager.UnRegisterPlayer(player.name);
        GameManager.RemoveDeadPlayer(player.name);
    }
}
