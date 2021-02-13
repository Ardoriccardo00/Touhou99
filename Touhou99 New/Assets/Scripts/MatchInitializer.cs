using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MatchInitializer : NetworkBehaviour
{
    public bool arenasSpawned = false;

    [SerializeField] GameObject arenaObject;
    [SerializeField] GameObject arenaCamera;
    [SerializeField] GameObject cameraParent;

    [SyncVar] List<GameObject> cameraList = new List<GameObject>();

    MyNewtworkManager nmref;
    [SerializeField] [SyncVar] int numOfPlayers = 0;

    private void Awake()
	{
        nmref = FindObjectOfType<MyNewtworkManager>();
		nmref.playerConnected += Nmref_aPlayerHasConnected;
		nmref.playerDisconnected += Nmref_playerDisconnected;
    }

	private void Nmref_playerDisconnected()
	{
        numOfPlayers--;
        print("A player has disconntected: now " + numOfPlayers + " players");
    }

	private void Nmref_aPlayerHasConnected()
	{
        numOfPlayers++;
        print("A player has conntected: now " + numOfPlayers + " players");
	}

	void Update()
    {
        if (arenasSpawned) return;

		if (Input.GetKeyDown(KeyCode.Space) && isServer)
		{
            RpcSpawnArenas();
            arenasSpawned = true;
		}
	}


    [ClientRpc]
    public void RpcSpawnArenas()
	{
        nmref.playerConnected -= Nmref_aPlayerHasConnected;
        nmref.playerDisconnected -= Nmref_playerDisconnected;

        int spawnNumber = -1;
        print("Spawn");

		float posX = -2.878f; //Increase 16.37
		float posY = 0.0027f;

		Camera.main.gameObject.SetActive(false);

        for (int i = 0; i < numOfPlayers; i++)
		{
            print(NetworkManager.singleton.numPlayers);

            spawnNumber++;
            GameObject newArena = Instantiate(arenaObject, new Vector2(posX, posY), Quaternion.identity);
            newArena.transform.localScale = new Vector3(11.57531f, 10.06353f, 16.32845f);

			foreach (GameObject cam in cameraList)
			{
                cam.SetActive(false);
			}

            GameObject newCamera = Instantiate(arenaCamera, new Vector2(posX, posY), Quaternion.identity);
            //newCamera.gameObject.SetActive(false);
            cameraList.Add(newCamera);
            newCamera.transform.SetParent(cameraParent.transform);

            //newCamera.transform.position = new Vector3(newCamera.transform.position.x, newCamera.transform.position.y, 0);
            //posX += 16.37f;
            posX += 19.37f;
        }
	}
}