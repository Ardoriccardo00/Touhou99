using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class EnemySpawner : NetworkBehaviour
{
    [Header ("Miste")]
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    playerMovement player;
    private int i = 0;

    [Header ("Timers")]
    GameObject gameCanvas;
    TimerToStartMatch timer;
    private float spawnDelay = 1f;
    private float spawnDelayCounter;
    private float timeBetweenSpawn;
    private float timeBetweenSpawnCounter;
    public float enemiesToSpawn;


    //public override void OnStartServer()
    //{
    //    timer = FindObjectOfType<TimerToStartMatch>();
    //    player = GameObject.FindObjectOfType<playerMovement>();
    //    //timeBetweenSpawn = UnityEngine.Random.Range(5f, 15f);
    //    //timeBetweenSpawnCounter = timeBetweenSpawn;
    //    spawnDelayCounter = spawnDelay;
    //}

    private void Start()
    {
        gameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");
        timer = gameCanvas.GetComponent<TimerToStartMatch>();
        player = GameObject.FindObjectOfType<playerMovement>();
        timeBetweenSpawn = UnityEngine.Random.Range(5f, 15f);
        timeBetweenSpawnCounter = timeBetweenSpawn;
        spawnDelayCounter = spawnDelay;
    }

    void Update()
    {
        if(timer.countDownToStart <= 0)
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
        
    }

    void Spawn()
    {
        var spawnPoint = Random.Range(0, spawnPoints.Length);
        var spawnPointPosition = spawnPoints[spawnPoint].transform.name;


        GameObject enemy = Instantiate(enemyPrefab, spawnPoints[spawnPoint].transform.position, spawnPoints[spawnPoint].transform.rotation);
        Enemy theEnemy = enemy.GetComponent<Enemy>();
        NetworkServer.Spawn(enemy);

        switch (spawnPointPosition)
        {
            case "UpLeft":
                theEnemy.spawnPosition = Enemy.SpawnPositionEnum.UpLeft;
                break;

            case "UpRight":
                theEnemy.spawnPosition = Enemy.SpawnPositionEnum.UpRight;
                break;
        }
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
/*public class EnemySpawner : NetworkBehaviour //fai spawnare i nemici al server che ottiene la posizione di tutti gli spanwers
{
    [Header("Oggetti")]
    public List<GameObject[]> spawnPoints = new List<GameObject[]>();
    //public List<int> list = new List<int>();
    public GameObject[] spawnPointsUpLeft;
    public GameObject[] spawnPointsUpRight;
    public GameObject enemyPrefab;
    //private Transform placeToSpawn;
    GameObject[] spawnerToUse;
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
        spawnPointsUpLeft = GameObject.FindGameObjectsWithTag("SpawnPointUpLeft");
        spawnPointsUpRight = GameObject.FindGameObjectsWithTag("SpawnPointUpRight");

        spawnPoints.Add(spawnPointsUpLeft);
        Debug.Log("added element: " + spawnPoints.Count);
        spawnPoints.Add(spawnPointsUpRight);
        Debug.Log("added element: " + spawnPoints.Count);
        RandomizeSpawner();
        FindClosestArena();

        foreach (GameObject[] element in spawnPoints)
        {
            Debug.Log(element);
        }

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
        foreach(GameObject element in spawnerToUse)
        {
            Debug.Log("element " + element + "local position " + element.transform.localPosition + "global position" + element.transform.position);
            GameObject enemy = Instantiate(enemyPrefab, element.transform.localPosition, element.transform.localRotation);
            NetworkServer.Spawn(enemy);
            enemy.transform.parent = theClosestArena.transform;
        }
        
    }

    void RandomizeSpawner()
    {
        j = UnityEngine.Random.Range(0, spawnPoints.Count);
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
}*/
