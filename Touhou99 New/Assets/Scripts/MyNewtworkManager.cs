using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNewtworkManager : NetworkManager
{
	public override void OnStartServer()
	{
		print("Servre Started");
	}

	public override void OnStopServer()
	{
		print("Server Stopped");
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		print("Connected");
	}

	public override void OnClientDisconnect(NetworkConnection conn)
	{
		print("Disconnected");
	}
}
