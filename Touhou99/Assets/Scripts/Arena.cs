using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class Arena : NetworkBehaviour 
{
    void Start()
    {
        RegisterArena();
        MoveInContainer();
    }

    void RegisterArena()
    {
        string _ID = "Arena " + GameManager.numberOfArenas;
        transform.name = _ID;
    }

    void MoveInContainer()
    {
        GameObject arenasContainer = GameObject.FindGameObjectWithTag("ArenasContainer");
        transform.SetParent(arenasContainer.transform);
    }
}
