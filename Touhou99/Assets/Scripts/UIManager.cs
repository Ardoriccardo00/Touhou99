using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Networking;

[Obsolete]
public class UIManager : NetworkBehaviour
{
    public Text hpText;
    public Text bombText;
    public playerMovement player;
    [Obsolete]
    //public playerMovement player;

    //void Start()
    //{
    //    player = GameObject.FindObjectOfType<playerMovement>();
    //}
    void Update()
    {
        //playerMovement player = GetComponent<playerMovement>();
        hpText.text = "HP: " + player.health + "/" + player.maxHealth;
        bombText.text = "Power: " + player.bombPower;
    }
}
