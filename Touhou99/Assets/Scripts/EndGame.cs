using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using UnityEngine.Networking;

[System.Obsolete]
public class EndGame : NetworkBehaviour
{
    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private Button startGameButton;

    [SerializeField]
    private Button leaveButton;

    [SerializeField]
    private ArenaSpawner arenaSpawner;

    int numberOfPlayers;

    private NetworkManager networkManager;

    [System.Obsolete]
    void Start()
    {
        networkManager = NetworkManager.singleton;
        gameOverText.enabled = false;
        leaveButton.gameObject.SetActive(false);
        arenaSpawner = FindObjectOfType<ArenaSpawner>();
    }

    void Update()
    {
        numberOfPlayers = GameManager.playersAlive.Count;

        if (numberOfPlayers <= 1)
        {
            gameOverText.enabled = true;
            leaveButton.gameObject.SetActive(true);
        }

        else
        {
            gameOverText.enabled = false;
            leaveButton.gameObject.SetActive(false);
        }
            
    }

    [Client]
    public void LeaveRoom()
    {
        MatchInfo matchInfo = networkManager.matchInfo;
        networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
        networkManager.StopHost();
    }

    public void ActivateArenaSpawner()
    {
        arenaSpawner.SpawnArenas();
    }
}
