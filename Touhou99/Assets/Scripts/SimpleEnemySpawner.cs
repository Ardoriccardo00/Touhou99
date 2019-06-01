using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class SimpleEnemySpawner : NetworkBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies;
    public Transform SpawnPosition;
    public Transform SpawnP;
    public override void OnStartServer()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, SpawnPosition.position, SpawnPosition.rotation);
            NetworkServer.Spawn(enemy);
        }
    }
}
