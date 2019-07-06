using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[Obsolete]
public class EnemySpawner : NetworkBehaviour
{
    [Header("Oggetti")]
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    private Transform placeToSpawn;
    Transform spawnerToUse;
    Arena theClosestArena;

    [Header("Timers")]
    private float spawnDelay = 0.3f;
    private float spawnDelayCounter;
    private float timeBetweenSpawn;
    private float timeBetweenSpawnCounter;
    public float enemiesToSpawn;

    private int i = 0;
    private int j;

    public override void OnStartServer()
    {
        timeBetweenSpawn = UnityEngine.Random.Range(1f, 3f);
        timeBetweenSpawnCounter = timeBetweenSpawn;

        spawnDelayCounter = spawnDelay;
    }
    private void Start()
    {
        RandomizeSpawner();
        FindClosestArena();
    }

    void Update()
    {

        if (timeBetweenSpawnCounter > 0)
        {
            timeBetweenSpawnCounter -= Time.deltaTime;
        }
        else if (timeBetweenSpawnCounter <= 0)
        {
            if (i < enemiesToSpawn)
            {
                spawnDelayCounter -= Time.deltaTime;

                if (spawnDelayCounter <= 0)
                {
                    CmdSpawn();
                    spawnDelayCounter = spawnDelay;
                    i++;
                }

            }
            if (i >= enemiesToSpawn)
            {

                timeBetweenSpawn = UnityEngine.Random.Range(1f, 3f);
                timeBetweenSpawnCounter = timeBetweenSpawn;
                RandomizeSpawner();
                i = 0;
            }
        }
    }

    //[Command]
    void CmdSpawn()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnerToUse.localPosition, spawnerToUse.localRotation);
        NetworkServer.Spawn(enemy);
        enemy.transform.parent = theClosestArena.transform;
    }

    void RandomizeSpawner()
    {
        j = UnityEngine.Random.Range(0, spawnPoints.Length);
        spawnerToUse = spawnPoints[j];
    }

    void FindClosestArena()
    {
        float distanceClosestArena = Mathf.Infinity;
        Arena closestArena = null;
        Arena[] allArenas = GameObject.FindObjectsOfType<Arena>();

        foreach (Arena currentArena in allArenas)
        {
            float distanceToArena = (currentArena.transform.position - this.transform.position).sqrMagnitude;

            if (distanceToArena < distanceClosestArena)
            {
                distanceClosestArena = distanceToArena;
                closestArena = currentArena;
                theClosestArena = closestArena;
            }
        }

        Debug.Log("Arena position: " + closestArena.transform.position + "Arena name: " + closestArena.transform.name);
    }
}



//private void CheckIfTimeToSpawn()
//{

//    if (player != null) //Se il giocatore esiste:
//    {
//        if (timeBetweenSpawnCounter <= 0) //Se il tempo casuale tra uno spawn e l'altro è 0: 
//        {

//        }
//    }
//}
//void TimerBetweenSpawn()
//{
//    while (true) { timeBetweenSpawnCounter -= Time.deltaTime; }
//}
//CheckIfTimeToSpawn();
//timeBetweenSpawn = UnityEngine.Random.Range(1f, 5f);
//timeBetweenSpawnCounter = timeBetweenSpawn;
//void Start()
//{
//    player = GameObject.FindObjectOfType<playerMovement>();
//    timeBetweenSpawn = UnityEngine.Random.Range(1f, 5f);
//    timeBetweenSpawnCounter = timeBetweenSpawn;
//    Enemy enemy = GetComponent<Enemy>();
//    spawnDelayCounter = spawnDelay;
//}
/*using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[Obsolete]
public class EnemySpawner : NetworkBehaviour
{
    //Miste
    public Transform spawnPoint;
    public GameObject enemyPrefab;
    playerMovement player;

    //Timer per lo spawn
    private float spawnDelay = 1f;
    private float spawnDelayCounter;
    public float timeBetweenSpawn;
    private float timeBetweenSpawnCounter;
    public float enemiesToSpawn;

    private int i = 0;

    public override void OnStartServer()
    {
        player = GameObject.FindObjectOfType<playerMovement>();
        timeBetweenSpawn = UnityEngine.Random.Range(5f, 15f);
        timeBetweenSpawnCounter = timeBetweenSpawn;
        spawnDelayCounter = spawnDelay;
    }

    void Update()
    {
        if (timeBetweenSpawnCounter > 0)
        {
            timeBetweenSpawnCounter -= Time.deltaTime;
        }
        else if (timeBetweenSpawnCounter <= 0)
        {
            if (i < enemiesToSpawn)
            {
                spawnDelayCounter -= Time.deltaTime;

                if (spawnDelayCounter <= 0)
                {
                    Spawn();
                    spawnDelayCounter = spawnDelay;
                    i++;
                }

            }
            if (i >= enemiesToSpawn)
            {
                timeBetweenSpawn = UnityEngine.Random.Range(5f, 10f);
                timeBetweenSpawnCounter = timeBetweenSpawn;
                i = 0;
            }
        }
    }


    void Spawn()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        NetworkServer.Spawn(enemy);
    }

}
*/
