using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Arena : NetworkBehaviour
{
    public ArenaSpawner ArenaSpawner;
    void Start()
    {
        RegisterArena();
        MoveInContainer();
    }

    void RegisterArena()
    {
        string _ID = "Spawn " + ArenaSpawner.spawnNumber;
        transform.name = _ID;
    }

    void MoveInContainer()
    {
        GameObject arenasContainer = GameObject.FindGameObjectWithTag("ArenasContainer");
        transform.SetParent(arenasContainer.transform);
    }
}
