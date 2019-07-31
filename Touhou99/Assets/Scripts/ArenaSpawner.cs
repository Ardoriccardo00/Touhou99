using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Obsolete]
public class ArenaSpawner : NetworkBehaviour
{
    public GameObject cameraPrefab;
    

    private int posX = 0;
    private int posY = 0;

    private float cameraPosX = 0.03f;
    private float cameraPosY = 0.85f;

    public static int spawnNumber = 1;

    private GameObject playerCamera;
    [SerializeField]
    private GameObject spawner;
    private GameObject spawn;

    [SerializeField]
    public GameObject arenaPrefab;
    public static int arenaNumber = 1;
    public static int cameraNumber = 1;

    private GameObject arena;
    private GameObject camera;

    public GameObject arenasContainer;
    public GameObject spawnsContainer;

    void Start()
    {
        arenasContainer = GameObject.Find("ArenasContainer");
        spawnsContainer = GameObject.Find("SpawnsContainer");
        posX = 0;
        posY = 0;
    }

    private void Update()
    {
        int numberOfPlayers = GameManager.playersAlive.Count;   
    }

    [Server]
    public void SpawnArenas()
    {
        foreach (KeyValuePair<string, playerMovement> entry in GameManager.playersAlive)
        {
            arena = Instantiate(arenaPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
            arena.transform.name = "Arena " + arenaNumber;
            NetworkServer.Spawn(arena);
            arena.transform.parent = arenasContainer.transform;

            arenaNumber += 1;
            posX += 17;
        }
    }

    [Server]
    public void SpawnCameras()
    {
        foreach (KeyValuePair<string, playerMovement> entry in GameManager.playersAlive)
        {
            camera = Instantiate(cameraPrefab, new Vector3(0, 0, -5), Quaternion.identity);
            CameraSettings();
            camera.transform.name = "Camera " + arenaNumber;
            NetworkServer.Spawn(camera);

            cameraNumber += 1;
            cameraPosX += 0.03f;
        }
    }

    private void CameraSettings()
    {
        camera.GetComponent<Camera>().rect = new Rect(cameraPosX, cameraPosY, 0.1f, 0.1f);
        //Cam2.rect = new Rect (0.5f, 0, 0.5f, 1);
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