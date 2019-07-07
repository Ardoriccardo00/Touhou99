using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Obsolete]
public class ArenaSpawner : NetworkBehaviour
{
    //public GameObject arenaPrefab;
    public GameObject cameraPrefab;

    [SerializeField]
    private int posX = 0;
    [SerializeField]
    private int posY = 0;

    public static int spawnNumber = 1;

    //private GameObject arena;
    private GameObject playerCamera;
    [SerializeField]
    private GameObject spawner;
    private GameObject spawn;

    [SerializeField]
    public GameObject arenaPrefab;
    public static int arenaNumber = 1;

    private GameObject arena;

    //[SerializeField]
    //private GameObject spawnButton;

    //[SerializeField]
    //private Transform spawnersParent;

    public GameObject arenasContainer;
    public GameObject spawnsContainer;


    void Awake()
    {
        arenasContainer = GameObject.Find("ArenasContainer");
        spawnsContainer = GameObject.Find("SpawnsContainer");
        SpawnSpawners();
        posX = 0;
        posY = 0;
    }

    private void Update()
    {
        int numberOfPlayers = GameManager.playersAlive.Count;   
    }

    public void SpawnSpawners()
    {
            for (int y = 0; y < 20; y++)
            {
                spawn = Instantiate(spawner, new Vector3(posX, posY, 0), Quaternion.identity);
                spawn.transform.name = "Spawn" + spawnNumber;
                spawn.transform.parent = spawnsContainer.transform;

            spawnNumber += 1;
                posX += 17;

            }       
    }

    [Command]
    public void CmdSpawnArenas()
    {
        foreach (KeyValuePair<string, playerMovement> entry in GameManager.playersAlive)
        {
            arena = Instantiate(arenaPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
            arena.transform.name = "Arena" + arenaNumber;
            NetworkServer.Spawn(arena);
            arena.transform.parent = arenasContainer.transform;

            arenaNumber += 1;
            posX += 17;
        }
    }
}



/*                //playerCamera = Instantiate(cameraPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
            //playerCamera.transform.name = "Camera" + arenaNumber;
            //playerCamera.transform.parent = arena.transform;*/
/*public void SpawnArenas()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int y = 0; y < 5; y++)
            {
                Debug.Log("istanzio");
                arena = Instantiate(arenaPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
                arena.transform.name = "Arena" + arenaNumber;

                arenaNumber += 1;
                posX += 17;

            }
            posY += 20;
            posX = 0;
        }
    }
*/
/*public void SpawnSpawners()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int y = 0; y < 5; y++)
            {
                spawn = Instantiate(spawner, new Vector3(posX, posY, 0), Quaternion.identity);
                spawn.transform.name = "Spawn" + spawnNumber;

                spawnNumber += 1;
                posX += 17;

            }
            posY += 20;
            posX = 0;
        }
    }
*/
/*[Command]
    void CmdSpawnArenas()
    {
            GameObject owner = this.gameObject;
            for (int i = 0; i < 5; i++)
            {
                for (int y = 0; y < 5; y++)
                {
                    arena = Instantiate(arenaPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
                    arena.transform.name = "Arena" + arenaNumber;
                    NetworkServer.Spawn(arena);
                    //NetworkServer.SpawnWithClientAuthority(arena, owner);

                    arenaNumber += 1;
                    posX += 17;

                }
                posY += 20;
                posX = 0;
            }

            spawnButton.SetActive(false);   
    }
*/