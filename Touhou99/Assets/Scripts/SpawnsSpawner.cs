using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnsSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject spawner;
    [SerializeField] playerMovement[] players;

    void Start()
    {
        players = FindObjectsOfType<playerMovement>();
        SpawnSpawners();
    }

    void Update()
    {
        
    }
    public void SpawnSpawners()
    {
        foreach (KeyValuePair<string, playerMovement> entry in GameManager.players)
        {
            print("GIOCATORE" + entry.Key + entry.Value);
        }

        for(int i = 0; i < players.Length; i++)
        {
            GameObject instantiatedSpawner = Instantiate(spawner, players[i].transform.position, Quaternion.identity);
            NetworkServer.Spawn(instantiatedSpawner);
            print("Spawnato spawner");
        }
    }
}
