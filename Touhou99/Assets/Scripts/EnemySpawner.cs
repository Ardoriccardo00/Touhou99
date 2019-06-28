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
                
                timeBetweenSpawn = UnityEngine.Random.Range(1f, 3f);
                timeBetweenSpawnCounter = timeBetweenSpawn;
                RandomizeSpawner();
                i = 0;
            }
        }
    }


    void Spawn()
    {
        
        GameObject enemy = Instantiate(enemyPrefab, spawnerToUse.position, spawnerToUse.rotation);
        NetworkServer.Spawn(enemy);
        //Debug.Log("Spawnato nemico");
    }

    void RandomizeSpawner()
    {
        j = UnityEngine.Random.Range(0, spawnPoints.Length);
        spawnerToUse = spawnPoints[j];
        //Debug.Log(j);
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