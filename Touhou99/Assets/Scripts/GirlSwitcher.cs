using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GirlSwitcher : NetworkBehaviour
{
    [HideInInspector] public NetworkCustom nc;
    public NetworkManager nm;
    public int girlNumber;
    private playerMovement player;
    void Start()
    {
        nc = FindObjectOfType<NetworkCustom>();
        nm = NetworkManager.singleton;
    }

    void Update()
    {
        
    }

    public void SetPlayer(playerMovement _player)
    {
        player = _player;
        print("bersaglio (girl switcher): " + _player);
    }

    //public void SwitchGirl(playerMovement _player, int _girlNumber)
    //{
    //    //_player = player;
    //    //_girlNumber = girlNumber; 
    //    nc.CmdChangeGirl(_player, _girlNumber);
    //}

    public void SwitchGirlFunciton()
    {
        //SwitchGirl(player, girlNumber);
        CmdChangeGirl(player, girlNumber);
        NetworkServer.Destroy(player.gameObject);
    }

    public void SetGirlNumber(int _girlNumber)
    {
        girlNumber = _girlNumber;
    }

    [Client]
    public void CmdChangeGirl(playerMovement _player, int _girl)
    {
        print("Players alive: " + GameManager.playersAlive.Count);
        GameManager.playersAlive.Remove(_player.transform.name);
        var conn = _player.connectionToClient;
        var newPlayer = Instantiate<GameObject>(nc.characters[_girl]);
        NetworkServer.ReplacePlayerForConnection(conn, newPlayer, 0);
    }
}

