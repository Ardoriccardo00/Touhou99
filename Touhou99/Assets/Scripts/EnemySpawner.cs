using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

[Obsolete]
public class EnemySpawner : NetworkBehaviour
{
    [Header ("Components")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;
    GameObject gameCanvas;
    Player player;
    private int i = 0;
    private GameObject enemiesContainer;

    [Header ("Timers")]
    TimerToStartMatch timer;
    private float spawnDelay = 1f;
    private float spawnDelayCounter;
    private float timeBetweenSpawn;
    private float timeBetweenSpawnCounter;
    [SerializeField] private int enemiesToSpawn;

    private void Start()
    {
        gameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");
        try { timer = gameCanvas.GetComponent<TimerToStartMatch>(); } catch { }
        player = GameObject.FindObjectOfType<Player>();
        timeBetweenSpawn = UnityEngine.Random.Range(2f, 8f);
        timeBetweenSpawnCounter = timeBetweenSpawn;
        spawnDelayCounter = spawnDelay;
        enemiesContainer = GameObject.FindGameObjectWithTag("EnemiesContainer");
    }

    void Update()
    {
        try
        {
            if (timer.countDownToStart <= 0)
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

        catch { }
    }


    void Spawn()
    {
        var spawnPoint = Random.Range(0, spawnPoints.Length);
        var spawnPointPosition = spawnPoints[spawnPoint].transform.name;


        GameObject enemy = Instantiate(enemyPrefab, spawnPoints[spawnPoint].transform.position, spawnPoints[spawnPoint].transform.rotation);
        Enemy theEnemy = enemy.GetComponent<Enemy>();
        NetworkServer.Spawn(enemy);
        enemy.transform.SetParent(enemiesContainer.transform);

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


