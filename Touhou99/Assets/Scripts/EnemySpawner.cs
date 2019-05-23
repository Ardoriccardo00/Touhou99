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
    //Miste
    public Transform spawnPoint;
    public GameObject enemyPrefab;
    playerMovement player;

    //Timer per lo spawn
    [SerializeField]
    public float timeBetweenSpawn;
    private float timeBetweenSpawnCounter;

    public float spawnDelay;
    private float spawnDelayCounter;

    public float enemiesToSpawn;

    private int i = 0;

    void Start()
    {
        player = GameObject.FindObjectOfType<playerMovement>();
        timeBetweenSpawn = UnityEngine.Random.Range(1f, 5f);
        timeBetweenSpawnCounter = timeBetweenSpawn;
        Enemy enemy = GetComponent<Enemy>();
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
            if(i<enemiesToSpawn)
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
                timeBetweenSpawn = UnityEngine.Random.Range(1f, 5f);
                timeBetweenSpawnCounter = timeBetweenSpawn;
                i = 0;
            }

        }
   
    }
    void Spawn()
    {
      Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);       
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