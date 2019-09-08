using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

[System.Obsolete]
public class EndGame : NetworkBehaviour
{
    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private Button startGameButton;

    [SerializeField]
    private ArenaSpawner arenaSpawner;

    [SerializeField] Text dataText;

    int numberOfPlayers;

    private NetworkManager networkManager;

    [System.Obsolete]
    void Start()
    {
        networkManager = NetworkManager.singleton;
        //gameOverText.enabled = false;
        //leaveButton.gameObject.SetActive(false);
        arenaSpawner = FindObjectOfType<ArenaSpawner>();
    }

    void Update()
    {
        numberOfPlayers = GameManager.playersAlive.Count;

        if (numberOfPlayers <= 1)
        {
            //gameOverText.enabled = true;
        }

        else
        {
            //gameOverText.enabled = false;
        }

        dataText.text = "Players Alive " +  Convert.ToString(GameManager.playersAlive.Count) + "Players connected " + Convert.ToString(GameManager.players.Count);
    }

    [Client]
    public void LeaveRoom()
    {
        MatchInfo matchInfo = networkManager.matchInfo;
        networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
        networkManager.StopHost();
        SceneManager.LoadScene(1);
    }

    public void ActivateArenaSpawner()
    {
        arenaSpawner.CmdSpawnArenas();
    }
}
