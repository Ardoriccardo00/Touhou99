using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject enemy;
    public float timeToSpawn;
    private float timeToSpawnCounter;

    void Start()
    {
        timeToSpawnCounter = timeToSpawn;
    }

    void Update()
    {
        timeToSpawnCounter -= Time.deltaTime;

        if (timeToSpawnCounter < 0f) { Spawn(); timeToSpawnCounter = timeToSpawn; }
    }

    void Spawn()
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
