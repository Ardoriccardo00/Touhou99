using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkGameManager : NetworkBehaviour
{
	[ClientRpc]
	public void SaySomething()
	{
		print("Something");
	}

	[ClientRpc]
	public void RpcDamagePlayer(PlayerIdentity player, float damage)
	{
		var targetPlayerHealth = player.GetComponent<Health>();
		targetPlayerHealth.CmdDecreaseHealth(damage);
	}

	[ClientRpc]
	public void RpcDestroyObject(GameObject objectToDestroy)
	{
		Destroy(objectToDestroy);
	}
}