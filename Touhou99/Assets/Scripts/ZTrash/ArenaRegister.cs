using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArenaRegister : NetworkBehaviour
{
    public ArenaSpawner ArenaSpawner;
    void Start()
    {
        RegisterArena();
    }

    void RegisterArena()
    {
        string _ID = "Spawn " + ArenaSpawner.spawnNumber;
        transform.name = _ID;
    }
}
