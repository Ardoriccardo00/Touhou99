using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Miste
    public Transform spawnPoint;
    public GameObject enemyPrefab;

    //Timer per lo spawn
    public float timeToSpawn;
    private float timeToSpawnCounter;
    private float spawnDelay;
    private float Delay = 1;
    public int enemiesToSpawn;

    //Direzione nemici
	//public Vector3 enemyDirection;
    //public string Direction;


    void Start()
    {
        //enemyDirection = new Vector3(0f, 0f);
        spawnDelay = Delay;
        Enemy enemy = GetComponent<Enemy>();

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

        timeToSpawnCounter = timeToSpawn;
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
        spawnDelay -= Time.deltaTime;
        for (int i = 0; i < enemiesToSpawn; i++) {
            spawnDelay -= Time.deltaTime;
            if (spawnDelay < 0f)
            {
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                spawnDelay = Delay;
            }
         }
    }
}
