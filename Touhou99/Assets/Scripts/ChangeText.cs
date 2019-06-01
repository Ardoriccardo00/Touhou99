using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    [Obsolete]
    playerMovement player;

    [Obsolete]
    void Start()
    {
        player = GameObject.FindObjectOfType<playerMovement>();
    }

    [Obsolete]
    void Update()
    {
        //Text myText = GameObject.Find("Canvas/Text").GetComponent<Text>();
        //Text myText2 = GameObject.Find("Canvas/Text2").GetComponent<Text>();
        //myText.text = "Numero casuale:" + spawner.timeBetweenSpawn + "Timer casuale:" + spawner.timeBetweenSpawnCounter;
        //myText2.text = "Spawn delay: " + spawner.spawnDelay + "Delay counter: " + spawner.spawnDelayCounter;
            Text myText = GameObject.Find("Canvas/Text").GetComponent<Text>();
            myText.text = Convert.ToString(player.bombPower);
    }
}
