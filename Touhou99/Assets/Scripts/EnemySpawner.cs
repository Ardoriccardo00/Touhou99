using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Miste
    public Transform spawnPoint;
    public GameObject enemyPrefab;

    //Timer per lo spawn
    public float timeToSpawn;
    private float timeToSpawnCounter;

    //Direzione nemici
	public Vector3 enemyDirection;
    public string Direction;


    void Start()
    {
        Direction = "";
        enemyDirection = new Vector3(0f, 0f);

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

        if (timeToSpawnCounter < 0f) { Spawn(); timeToSpawnCounter = timeToSpawn; }
    }

    void Spawn()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
