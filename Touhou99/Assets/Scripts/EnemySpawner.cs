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
    public float timeToSpawn;
    private float timeToSpawnCounter;
    private float timeBetweenSpawn;
    private float timeBetweenSpawnCounter;

    playerMovement player;

    public float enemiesToSpawn;
    private float enemiesToSpawnCounter;

    void Start()
    {
        player = GameObject.FindObjectOfType<playerMovement>();
        timeBetweenSpawn = UnityEngine.Random.Range(1f, 5f);
        timeBetweenSpawnCounter = timeBetweenSpawn;
        Enemy enemy = GetComponent<Enemy>();
        timeToSpawnCounter = timeToSpawn;
        enemiesToSpawnCounter = enemiesToSpawn;
    }

    void Update()
    {
        timeBetweenSpawnCounter -= Time.deltaTime;
        //timeToSpawnCounter -= Time.deltaTime;
        CheckIfTimeToSpawn();


    }

    private void CheckIfTimeToSpawn()
    {
        if (player != null) //Se il giocatore esiste:
        {
            if (timeBetweenSpawnCounter <= 0) //Se il tempo casuale tra uno spawn e l'altro è 0: 
            {
                timeToSpawnCounter -= Time.deltaTime; //Il tempo per spawnare le file di nemici diminuisce
                if (timeToSpawnCounter <= 0f) // ...e se è 0:
                {
                    enemiesToSpawnCounter--; //il contatore di nemici da spawnare scende

                    if (enemiesToSpawnCounter > 0) //Se è meno di 0:
                    {
                        Spawn();
                        
                    }

                    timeToSpawnCounter = timeToSpawn;

                }

                Spawn();
                timeBetweenSpawn = UnityEngine.Random.Range(1f, 5f);
                timeBetweenSpawnCounter = timeBetweenSpawn;
            }
        }
    }

    void Spawn()
    {
      Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);       
    }
}
