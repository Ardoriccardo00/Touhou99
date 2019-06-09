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
    public Text nameText;
    public playerMovement player;
    public HostGame hg;

    [Obsolete]
    //public playerMovement player;

    void Start()
    {
        hg = GetComponent<HostGame>();
    }
    void Update()
    {
        //playerMovement player = GetComponent<playerMovement>();
        hpText.text = "HP: " + player.health + "/" + player.maxHealth;
        bombText.text = "Power: " + player.bombPower;
    }
}
