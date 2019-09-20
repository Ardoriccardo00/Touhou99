using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnsSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject spawner;
    [SerializeField] Player[] players;

    void Start()
    {
        players = FindObjectsOfType<Player>();
        SpawnSpawners();
    }

    void Update()
    {
        
    }
    public void SpawnSpawners()
    {
        foreach (KeyValuePair<string, Player> entry in GameManager.players)
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
