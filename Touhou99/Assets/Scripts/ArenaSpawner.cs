using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Obsolete]
public class ArenaSpawner : MonoBehaviour
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


    void Awake()
    {
        SpawnSpawners();
        posX = 0;
        posY = 0;
    }

    public void SpawnSpawners()
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
    public void SpawnArenas()
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

}



/*                //playerCamera = Instantiate(cameraPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
            //playerCamera.transform.name = "Camera" + arenaNumber;
            //playerCamera.transform.parent = arena.transform;*/
