using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CloneSpawner : NetworkBehaviour
{
    public GameObject clonePrefab;
    [SerializeField] GameObject cloneSpawnPoint;

   public void InstantiateClone()
    {
        GameObject clone = Instantiate(clonePrefab, cloneSpawnPoint.transform.position, cloneSpawnPoint.transform.rotation);
        NetworkServer.Spawn(clone);
    }
}
