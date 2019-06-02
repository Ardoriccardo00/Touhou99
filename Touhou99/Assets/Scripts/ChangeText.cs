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
        //player = GameObject.FindObjectOfType<playerMovement>();
        //Text myText = GameObject.Find("Canvas/Text").GetComponent<Text>();
        //myText.text = Convert.ToString(player.health);
    }

    [Obsolete]
    void Update()
    {
        playerMovement player = GetComponent<playerMovement>();
        Text myText = GameObject.Find("Canvas/Text").GetComponent<Text>();
        myText.text = Convert.ToString(player.health);
    }
}
