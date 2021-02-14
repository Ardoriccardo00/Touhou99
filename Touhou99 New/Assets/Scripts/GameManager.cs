using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class GameManager : NetworkBehaviour
{
    public bool arenasSpawned = false;

    [SerializeField] GameObject arenaObject;

    [SyncVar] List<GameObject> cameraList = new List<GameObject>();

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
        /*float posX = -2.878f; //Increase 16.37
		float posY = 0.0027f;*/

        /*float posX = -6.87f; //Increase 16.37
        float posY = 0.86f;*/

        float posX = -2.89f; //Increase 16.37
        float posY = 0f;

         Camera.main.gameObject.SetActive(false);

        for (int i = 0; i < playerDictionary.Count; i++)
		{
            GameObject newArena = Instantiate(arenaObject, new Vector2(posX, posY), Quaternion.identity);
            newArena.transform.localScale = new Vector3(11.57531f, 10.06353f, 16.32845f);

            PlayerIdentity currentPlayer = playerDictionary.Values.ElementAt(i); //Get current player
            PlayerCameraBehaviour currentPlayerCamera = currentPlayer.transform.FindChild("Game Camera").GetComponent<PlayerCameraBehaviour>(); //Get current player camera
            currentPlayerCamera.gameObject.SetActive(true); //Activates camera
            currentPlayer.transform.position = newArena.GetComponentInChildren<ArenaCenter>().transform.position; //moves the player
            currentPlayerCamera.SetPosition(currentPlayer.transform.position);
            //currentPlayerCamera.transform.position = currentPlayer.transform.position; //Moves the camera
            currentPlayer.GetComponent<PlayerMovement>().enabled = true;

            posX += 19.37f;
        }
    }
}