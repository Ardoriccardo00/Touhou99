using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    //Miste
    public Transform spawnPoint;
    public GameObject enemyPrefab;

    //Timer per lo spawn
    
    public float timeBetweenSpawn;
    public float timeBetweenSpawnCounter;

    public float spawnDelay;
    public float spawnDelayCounter;

    playerMovement player;

    public float enemiesToSpawn;
    public float enemiesToSpawnCounter;

    public int i = 0;

    void Start()
    {
        player = GameObject.FindObjectOfType<playerMovement>();
        timeBetweenSpawn = UnityEngine.Random.Range(1f, 5f);
        timeBetweenSpawnCounter = timeBetweenSpawn;
        Enemy enemy = GetComponent<Enemy>();
        spawnDelayCounter = spawnDelay;
        enemiesToSpawnCounter = enemiesToSpawn;
        //TimerBetweenSpawn();
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
        //CheckIfTimeToSpawn();
        //timeBetweenSpawn = UnityEngine.Random.Range(1f, 5f);
        //timeBetweenSpawnCounter = timeBetweenSpawn;
    }

    private void CheckIfTimeToSpawn()
    {

        if (player != null) //Se il giocatore esiste:
        {
            if (timeBetweenSpawnCounter <= 0) //Se il tempo casuale tra uno spawn e l'altro è 0: 
            {
                
            }
        }
    }

    void TimerBetweenSpawn()
    {
        while (true) { timeBetweenSpawnCounter -= Time.deltaTime; }
    }
    void Spawn()
    {
      Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);       
    }
}
