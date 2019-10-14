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
        RegisterCloneSpawner();
    }

    void RegisterArena()
    {
        if (isServer) RpcRegisterArena();
        else CmdRegisterArena();
    }

    [Command]
    void CmdRegisterArena()
    {
        string _ID = "Arena " + GameManager.numberOfArenas;
        transform.name = _ID;
    }

    [ClientRpc]
    void RpcRegisterArena()
    {
        string _ID = "Arena " + GameManager.numberOfArenas;
        transform.name = _ID;
    }

    void RegisterCloneSpawner()
    {
        GameObject cloneSpawner = transform.Find("CloneSpawnPoint").gameObject;
        cloneSpawner.transform.name = "CloneSpawnPoint " + GameManager.numberOfArenas;
    }

    void MoveInContainer()
    {
        GameObject arenasContainer = GameObject.FindGameObjectWithTag("ArenasContainer");
        transform.SetParent(arenasContainer.transform);
    }
}
