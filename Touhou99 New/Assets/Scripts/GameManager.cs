using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class GameManager : NetworkBehaviour
{
    public bool arenasSpawned = false;

    [SerializeField] GameObject arenaObject;
    //[SerializeField] GameObject arenaCamera;
    //[SerializeField] GameObject cameraParent;

    [SyncVar] List<GameObject> cameraList = new List<GameObject>();

    //MyNewtworkManager nmref;
    //[SerializeField] [SyncVar] int numOfPlayers = 0;
    [SerializeField] [SyncVar] List<GameObject> arenaCenterList = new List<GameObject>();

    [SyncVar] int i;

    [Header("GameManager")]
    public static Dictionary<string, PlayerIdentity> playerDictionary = new Dictionary<string, PlayerIdentity>();

    private void Awake()
    {
        /*nmref = FindObjectOfType<MyNewtworkManager>();
        nmref.playerConnected += Nmref_aPlayerHasConnected;
        nmref.playerDisconnected += Nmref_playerDisconnected;*/
    }

    #region GameManager

    /*private void Nmref_playerDisconnected()
    {
        numOfPlayers--;
        print("A player has disconntected: now " + numOfPlayers + " players");
    }

    private void Nmref_aPlayerHasConnected()
    {
        numOfPlayers++;
        print("A player has conntected: now " + numOfPlayers + " players");
    }*/

    public static void RegisterPlayer(string playerNetId, PlayerIdentity playerToRegister)
	{
        string playerId = "Player " + playerNetId;
        playerDictionary.Add(playerId, playerToRegister);
        playerToRegister.transform.name = playerId;

        print(playerId + "Has joined the game");
        print("players on the dictionary: " + playerDictionary.Count);
	}

    public static void UnRegisterPlayer(string playerNetId)
    {
        string playerId = "Player " + playerNetId;
        playerDictionary.Remove(playerId);

        print(playerId + "Has left the game");
        print("players on the dictionary: " + playerDictionary.Count);
    }

	#endregion

	[System.Obsolete]
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
	[System.Obsolete]
	public void RpcSpawnArenas()
	{
        /*nmref.playerConnected -= Nmref_aPlayerHasConnected;
        nmref.playerDisconnected -= Nmref_playerDisconnected;*/

        int spawnNumber = -1;
        print("Spawn");

		/*float posX = -2.878f; //Increase 16.37
		float posY = 0.0027f;*/

        float posX = -6.87f; //Increase 16.37
        float posY = 0.86f;

        Camera.main.gameObject.SetActive(false);

        for (i = 0; i < playerDictionary.Count; i++)
		{
            //print(NetworkManager.singleton.numPlayers);

            spawnNumber++;
            GameObject newArena = Instantiate(arenaObject, new Vector2(posX, posY), Quaternion.identity);
            newArena.transform.localScale = new Vector3(11.57531f, 10.06353f, 16.32845f);

            var currentPlayer = playerDictionary.Values.ElementAt(i);
            //print(playerDictionary.Values.ElementAt(i).GetComponent<PlayerCameraBehaviour>());
            print(currentPlayer.transform.name);
            //playerDictionary.Values.ElementAt(i).GetComponent<PlayerCameraBehaviour>().gameObject.SetActive(true);
            currentPlayer.transform.FindChild("Game Camera").gameObject.SetActive(true);
            currentPlayer.transform.position = newArena.GetComponentInChildren<ArenaCenter>().transform.position;

            /*GameObject newCamera = Instantiate(arenaCamera, new Vector2(posX, posY), Quaternion.identity);
            cameraList.Add(newCamera);
            newCamera.transform.SetParent(cameraParent.transform);

            foreach (GameObject cam in cameraList) //Disables all cameras
			{
                cam.SetActive(false);
			}

            cameraList[i].SetActive(true);*/

            //newCamera.transform.position = new Vector3(newCamera.transform.position.x, newCamera.transform.position.y, 0);
            //posX += 16.37f;
            posX += 19.37f;
        }
    }
}