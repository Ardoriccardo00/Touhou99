using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerIdentity : NetworkBehaviour
{
    [SerializeField] string playerName = "null";
    PlayerMovement playerMovement; 

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

    public string ReturnPlayerName()
	{
        return playerName;
	}

    public PlayerMovement ReturnMovementComponent()
	{
        return playerMovement;
	}
}
