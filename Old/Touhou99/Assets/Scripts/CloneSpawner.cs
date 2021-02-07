using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class CloneSpawner : NetworkBehaviour
{
    public GameObject clonePrefab;
    [SerializeField] GameObject cloneSpawnPoint;

    [System.Obsolete]
    public void InstantiateClone()
    {
        GameObject clone = Instantiate(clonePrefab, cloneSpawnPoint.transform.position, cloneSpawnPoint.transform.rotation);
        NetworkServer.Spawn(clone);
    }
}
