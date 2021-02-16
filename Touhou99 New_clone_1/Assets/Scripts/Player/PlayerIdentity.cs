using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PlayerIdentity : NetworkBehaviour
{
    [SerializeField] string playerName = "null";
    PlayerMovement playerMovement;
	string thisNetId = null;

    void Start()
    {
		if (isLocalPlayer)
		{
            GetComponent<SpriteRenderer>().color = Color.blue;
		}
		else
		{
            GetComponent<SpriteRenderer>().color = Color.red;
        } 

        playerMovement = GetComponent<PlayerMovement>();
    }

	public override void OnStartClient()
	{
		base.OnStartClient();

		thisNetId = Convert.ToString(GetComponent<NetworkIdentity>().netId);

		GameManager.RegisterPlayer(thisNetId, this);
	}

	public override void OnStopClient()
	{
		base.OnStopClient();

		GameManager.UnRegisterPlayer(thisNetId);
	}

	public string ReturnPlayerName()
	{
        return playerName;
	}

    public PlayerMovement ReturnMovementComponent()
	{
        return playerMovement;
	}
}
