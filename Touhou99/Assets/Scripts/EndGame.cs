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
    int numberOfPlayers;

    [System.Obsolete]
    NetworkManager nm;

    [System.Obsolete]
    void Start()
    {
        //nm = FindObjectOfType<NetworkManager>();
        nm = NetworkManager.singleton;
        gameOverText.enabled = false;
        numberOfPlayers = nm.numPlayers; 
    }

    void Update()
    {
        Debug.Log("numero giocatori" + nm.numPlayers);
        //if (numberOfPlayers == 1)
        //{
        //    gameOverText.enabled = true;
        //}

        //else
        //    gameOverText.enabled = false;
    }
}
