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

    //Direzione nemici
	//public Vector3 enemyDirection;
    //public string Direction;


    void Start()
    {
        Enemy enemy = GetComponent<Enemy>();
        timeToSpawnCounter = timeToSpawn;

        /* switch (Direction)
         {
             case "right":
                 //enemy.moveDirection = enemy.directionRight;
                 enemyDirection = enemy.directionRight;
                 break;
             case "left":
                 //enemy.moveDirection = enemy.directionLeft;
                 break;
             case "up":
                 //enemy.moveDirection = enemy.directionUp;
                 break;
             case "down":
                 //enemy.moveDirection = enemy.directionDown;
                 break;
         }*/
    }

    void Update()
    {
        timeToSpawnCounter -= Time.deltaTime;
        
          if (timeToSpawnCounter < 0f)
            {
                Spawn();
                timeToSpawnCounter = timeToSpawn;
            }
    }

    void Spawn()
    {
      Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);       
    }
}
