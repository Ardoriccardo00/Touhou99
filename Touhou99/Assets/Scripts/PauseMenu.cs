using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;

[System.Obsolete]
public class PauseMenu : MonoBehaviour
{
    public static bool IsOn = false;

    private NetworkManager networkManager;

    private PlayerUI pu;

    void Start()
    {
        networkManager = NetworkManager.singleton;
        pu = GetComponentInParent<PlayerUI>();
    }

    public void LeaveRoom()
    {

        MatchInfo matchInfo = networkManager.matchInfo;
        //GameManager.UnRegisterPlayer(transform.name);
        //GameManager.RemoveDeadPlayer(transform.name);
        networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
        networkManager.StopHost();
        SceneManager.LoadScene(1);
    }
}
