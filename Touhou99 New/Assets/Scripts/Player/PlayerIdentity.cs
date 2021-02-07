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
