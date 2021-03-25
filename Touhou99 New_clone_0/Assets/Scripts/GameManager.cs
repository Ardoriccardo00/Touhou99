using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class GameManager : NetworkBehaviour
{
    public bool arenasSpawned = false;

    [SerializeField] GameObject arenaObject;

    [SerializeField] [SyncVar] List<GameObject> arenaCenterList = new List<GameObject>();

    [Header("GameManager")]
    public static Dictionary<string, PlayerIdentity> playerDictionary = new Dictionary<string, PlayerIdentity>();

    #region GameManager

    public static void RegisterPlayer(string playerNetId, PlayerIdentity playerToRegister)
	{
        string playerId = "Player " + playerNetId;
        playerDictionary.Add(playerId, playerToRegister);
        playerToRegister.transform.name = playerId;
	}

    public static void UnRegisterPlayer(string playerNetId)
    {
        string playerId = "Player " + playerNetId;
        playerDictionary.Remove(playerId);
    }

	#endregion

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
        float posX = -2.89f; //Increase 16.37
        float posY = 0f;

         Camera.main.gameObject.SetActive(false);

        for (int i = 0; i < playerDictionary.Count; i++)
		{
            GameObject newArena = Instantiate(arenaObject, new Vector2(posX, posY), Quaternion.identity);
            newArena.transform.localScale = new Vector3(11.57531f, 10.06353f, 16.32845f);
            GameObject enemySpawner = newArena.transform.Find("Enemy Spawn Point").gameObject;

            NetworkServer.Spawn(enemySpawner);
            //CmdSpawnEnemySpawner(enemySpawner);      

            PlayerIdentity currentPlayer = playerDictionary.Values.ElementAt(i); //Get current player
            PlayerCameraBehaviour currentPlayerCamera = currentPlayer.transform.Find("Game Camera").GetComponent<PlayerCameraBehaviour>(); //Get current player camera
            PlayerWeapon currentPlayerWeapon = currentPlayer.GetComponent<PlayerWeapon>();
            currentPlayerCamera.gameObject.SetActive(true); //Activates camera
            currentPlayerCamera.transform.name = currentPlayer.transform.name + " camera"; //Renames camera
            currentPlayerCamera.GetComponent<AudioListener>().enabled = true;
            currentPlayer.transform.position = newArena.GetComponentInChildren<ArenaCenter>().transform.position; //moves the player
            currentPlayerCamera.SetPosition(currentPlayer.transform.position); //Set the position of the child player camera
            //currentPlayer.GetComponent<EnemyCameraSwitcher>().SwitchEnemyCamera(false); //Sets the camera i guess
            //currentPlayerCamera.transform.position = currentPlayer.transform.position; //Moves the camera
            currentPlayer.GetComponent<PlayerMovement>().enabled = true; //Enables player movement
            currentPlayerWeapon.enabled = true; //Enables Player weapon
            currentPlayerWeapon.FindOwnPlayerArena(); //The player weapon finds the closest clone spawn point
            //currentPlayer.GetComponent<EnemyCameraSwitcher>().CreateCameraList(); //Creates a list of cameras in the scene for all players

            posX += 19.37f;
        }

        RpcSetCameraLists();
    }

    [ClientRpc]
    void RpcSetCameraLists()
	{
		for (int i = 0; i < playerDictionary.Count; i++)
		{
            EnemyCameraSwitcher currentPlayer = playerDictionary.Values.ElementAt(i).GetComponent<EnemyCameraSwitcher>();
            currentPlayer.CreateCameraList();
        }
	}
}