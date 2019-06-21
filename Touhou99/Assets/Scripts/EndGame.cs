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
    void Start()
    {
        gameOverText.enabled = false;
    }

    void Update()
    {
        numberOfPlayers = NetworkManager.singleton.numPlayers;
        //Debug.Log(numberOfPlayers);

        if (numberOfPlayers == 1)
        {
            gameOverText.enabled = true;
        }

        else
            gameOverText.enabled = false;

    }
}
