using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class MyNewtworkManager : NetworkManager
{
	/*public delegate void PlayerJoinOrLeave();
	public event PlayerJoinOrLeave playerConnected;
	public event PlayerJoinOrLeave playerDisconnected;*/

	public override void OnStartClient()
	{
		print("Connected");
	}

	public override void OnServerAddPlayer(NetworkConnection conn)
	{
		print("Added player");
		//playerConnected();

		Transform startPos = GetStartPosition();
		GameObject player = startPos != null
			? Instantiate(playerPrefab, startPos.position, startPos.rotation)
			: Instantiate(playerPrefab);

		NetworkServer.AddPlayerForConnection(conn, player);
	}

	public override void OnClientDisconnect(NetworkConnection conn)
	{
		print("A player left");
		//playerDisconnected();
		base.OnClientDisconnect(conn);
	}

	/*public override void OnStopHost()
	{
		base.OnStopHost();
		print("Stop host");
		StopClient();
	}

	public override void OnStopServer()
	{
		base.OnStopServer();
	}

	public override void OnStopClient()
	{
		base.OnStopClient();
		print("Stop client");
		SceneManager.LoadScene(2);
	}*/
}
