﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class ArenaRegister : NetworkBehaviour
{
    public ArenaSpawner ArenaSpawner;
    void Start()
    {
        RegisterArena();
    }

    void RegisterArena()
    {
        string _ID = "Arena " + ArenaSpawner.arenaNumber;
        transform.name = _ID;
    }
}