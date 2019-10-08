using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;
using TMPro;

[System.Obsolete]
public class GameCanvas : NetworkBehaviour
{
    [Header("Components")]
    [SerializeField] private Text gameOverText;
    [SerializeField] private Button startGameButton;
    [SerializeField] private ArenaSpawner arenaSpawner;
    [SerializeField] Text dataText;
    [SerializeField] TextMeshProUGUI countDown;
    [SerializeField] TimerToStartMatch timer;
    [SerializeField] GameObject centerPoint;

    [Header("Others")]
    int numberOfPlayers;
    private NetworkManager networkManager;
    PlayerSetup[] playerSetups;
    bool textCanMove = false;

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
        print(textCanMove);
        if (textCanMove == true)
        {
            print("can");
            countDown.transform.position = Vector3.MoveTowards(countDown.transform.position, centerPoint.transform.position, 1000f * Time.deltaTime);
        }

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
        var theTimer = timer.countDownToStart;
        countDown.text = Convert.ToString(Mathf.Round((theTimer)));

        if (timer.countDownToStart <= 0)
        {
            countDown.enabled = false;
        }
    }

    public void MoveTimerText(bool can)
    {
        textCanMove = can;
    }

    public void LeaveRoom()
    {
        MatchInfo matchInfo = networkManager.matchInfo;
        networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
        networkManager.StopHost();
    }
}
