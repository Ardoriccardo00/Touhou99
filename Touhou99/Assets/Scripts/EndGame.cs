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
    [Header("Components")]
    [SerializeField] private Text gameOverText;
    [SerializeField] private Button startGameButton;
    [SerializeField] private ArenaSpawner arenaSpawner;
    [SerializeField] Text dataText;
    [SerializeField] Text countDown;
    [SerializeField] TimerToStartMatch timer;

    [Header("Components")]
    int numberOfPlayers;
    private NetworkManager networkManager;
    PlayerSetup[] playerSetups;

    void Start()
    {
        networkManager = NetworkManager.singleton;
        gameOverText.enabled = false;
        arenaSpawner = FindObjectOfType<ArenaSpawner>();
        playerSetups = FindObjectsOfType<PlayerSetup>();

        foreach(PlayerSetup player in playerSetups)
        {
            if (player.isServer)
            {
                gameObject.SetActive(true);
                print("Attivata UI");
            }
            else
            {
                gameObject.SetActive(false);
                print("Disattivata UI");
            }
        }
    }

    void Update()
    {
        CheckCountDownTimer();
        CheckIfActivateGameOverText();

        dataText.text = "Players Alive " + Convert.ToString(GameManager.playersAlive.Count) + "Players connected " + Convert.ToString(GameManager.players.Count);
    }

    private void CheckIfActivateGameOverText()
    {
        numberOfPlayers = GameManager.playersAlive.Count;

        if (numberOfPlayers <= 1)
        {
            gameOverText.enabled = true;
        }

        else
        {
            gameOverText.enabled = false;
        }
    }

    private void CheckCountDownTimer()
    {
        countDown.text = Convert.ToString(timer.countDownToStart);

        if (timer.countDownToStart <= 0)
        {
            countDown.enabled = false;
        }
    }

    public void LeaveRoom()
    {
        MatchInfo matchInfo = networkManager.matchInfo;
        networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
        networkManager.StopHost();
    }
}
